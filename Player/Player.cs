using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private StageData stage;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject chargedBullet;
    [SerializeField] private KeyCode left;
    [SerializeField] private KeyCode right;
    [SerializeField] private KeyCode attack;
    [SerializeField] private KeyCode jump;
    [SerializeField] private KeyCode dash;
    [SerializeField] private KeyCode action;
    [SerializeField] private Animator anime;
    private AudioSource audioSource;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip chargeCompleteSound;
    [SerializeField] private AudioClip chargeShootSound;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip dashSound;
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip hitSound;
    private int dir;
    private float startTime;
    private bool chargeComplete = true;

    private void Start()
    {
        dir = 1;
        audioSource = GetComponent<AudioSource>();
        Key();
    }

    private void playSound(string cond)
    {
        switch (cond)
        {
            case"Shoot":
                audioSource.clip = shootSound;
                break;
            case "ChargeComplete":
                audioSource.clip = chargeCompleteSound;
                break;
            case "ChargeShoot":
                audioSource.clip = chargeShootSound;
                break;
            case "Attack":
                audioSource.clip = attackSound;
                break;
            case "Hit":
                audioSource.clip = hitSound;
                break;
        }
        audioSource.Play();
    }

    private void Update()
    {
        if (anime.GetBool("isDying")) return;
        if (gameObject.GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            anime.SetBool("isJumping", false);
            anime.SetBool("isFalling", true);
        }    
        else anime.SetBool("isFalling", false);
        if (PlayerPrefs.GetInt("DASH") > 0)
        {
            if (Input.GetKeyDown(dash))
            {
                audioSource.clip = dashSound;
                GetComponent<Movement2D>().Dash(dir, anime, audioSource);
                anime.SetBool("isFalling", true);
            }
        }
        if (Input.GetKeyDown(jump))
        {
            audioSource.clip = jumpSound;
            gameObject.GetComponent<Movement2D>().Jump(anime, audioSource);
        }
        bool l = Input.GetKey(left);
        bool r = Input.GetKey(right);
        anime.SetBool("isRunning", l || r);
        if (l) dir = -1; else if (r) dir = 1;
        transform.rotation = Quaternion.Euler(0, 90-90*dir, 0);
        transform.position += new Vector3(anime.GetBool("isRunning") ? dir : 0, 0, 0) * speed * Time.deltaTime;
        if (Input.GetKeyDown(attack))
        {
            playSound("Shoot");
            startTime = Time.time;
            chargeComplete = false;
            GameObject bullet = Instantiate(bulletPrefab, new Vector3(transform.position.x + 0.5f*dir, transform.position.y, 0), Quaternion.identity);
            bullet.GetComponent<Movement2D>().Setup(10.0f, new Vector3(dir, 0, 0));
            Destroy(bullet, 2.0f);
        }
        if (PlayerPrefs.GetInt("CHARGESHOT") > 0)
        {
            if(chargeComplete == false && Time.time - startTime >= 0.7f)
            {
                playSound("ChargeComplete");
                chargeComplete = true;
            }
            if (Input.GetKeyUp(attack))
            {
                if (chargeComplete == true)
                {
                    playSound("ChargeShoot");
                    GameObject charge = Instantiate(chargedBullet, new Vector3(transform.position.x + 0.5f * dir, transform.position.y, 0), Quaternion.identity);
                    charge.GetComponent<Movement2D>().Setup(20.0f, new Vector3(dir, 0, 0));
                    Destroy(charge, 1.0f);
                }
                else chargeComplete = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (anime.GetBool("isDying")) return;
        string t = collision.gameObject.tag;
        if(t.Length > 5)
        {
            if (t.Substring(0, 5) == "patte" || t.Substring(0, 5) == "enemy" || t.Substring(0, 5) == "bosss")
            {
                playSound("Hit");
                GetComponent<PlayerLevel>().TakeDamage(int.Parse(t.Substring(5)), anime);
                if (t.Substring(0, 5) == "patte")
                {
                    Destroy(collision.gameObject);
                }
            }
        }
        else
        {
            anime.SetBool("isJumping", false);
            anime.SetBool("isFalling", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (anime.GetBool("isDying")) return;
        string t = collision.gameObject.tag;
        if (t.Length > 5)
        {
            if (t.Substring(0, 5) == "patte" || t.Substring(0, 5) == "enemy" || t.Substring(0, 5) == "bosss")
            {
                playSound("Hit");
                GetComponent<PlayerLevel>().TakeDamage(int.Parse(t.Substring(5)), anime);
                if (t.Substring(0, 5) == "patte")
                {
                    Destroy(collision.gameObject);
                }
            }
        }
    }

    public void Key()
    {
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("LEFT"), true);
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RIGHT"), true);
        jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JUMP"), true);
        attack = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ATTACK"), true);
        dash = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("DASH"), true);
        action = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ACTION"), true);
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, stage.LimitMin.x, stage.LimitMax.x),
                                         Mathf.Clamp(transform.position.y, stage.LimitMin.y, stage.LimitMax.y));
    }
}
