using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;
    public StageData stage;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject chargedBullet;
    [SerializeField] KeyCode left;
    [SerializeField] KeyCode right;
    [SerializeField] KeyCode attack;
    [SerializeField] KeyCode jump;
    [SerializeField] KeyCode down;
    [SerializeField] bool canDown;
    [SerializeField] KeyCode dash;
    [SerializeField] KeyCode heal;
    [SerializeField] KeyCode action;
    [SerializeField] Animator anime;
    AudioSource audioSource;
    [SerializeField] AudioClip shootSound;
    [SerializeField] AudioClip chargeCompleteSound;
    [SerializeField] AudioClip chargeShootSound;
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip dashSound;
    [SerializeField] AudioClip healSound;
    [SerializeField] AudioClip attackSound;
    [SerializeField] AudioClip hitSound;
    int dir;
    float startTime;
    bool chargeComplete = true;
    bool isAlreadyHurt = false;
    bool ceiling;

    void Start()
    {
        dir = 1;
        audioSource = GetComponent<AudioSource>();
        Key();
    }

    void playSound(string cond)
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

    void Update()
    {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("EFFECT");
        if (anime.GetBool("isDying")) return;
        if (GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            GetComponent<Movement2D>().jumpCount = GetComponent<Movement2D>().jumpCount == 2 ? 2 : 1;
            anime.SetBool("isJumping", false);
            anime.SetBool("isFalling", true);
        }
        else if (GetComponent<Rigidbody2D>().velocity.y == 0)
        {
            if (!ceiling && !GetComponent<Movement2D>().doNotDash)
            {
                GetComponent<Movement2D>().jumpCount = 0;
            }
            anime.SetBool("isFalling", false);
            anime.SetBool("isJumping", false);
        }
        else
        {
            anime.SetBool("isFalling", false);
        }
        if (PlayerPrefs.GetInt("DASHLV") > 0)
        {
            if (Input.GetKeyDown(dash))
            {
                audioSource.clip = dashSound;
                GetComponent<Movement2D>().Dash(dir, anime, audioSource);
                anime.SetBool("isFalling", true);
            }
        }
        if (PlayerPrefs.GetInt("HEALLV") > 0)
        {
            if (Input.GetKeyDown(heal))
            {
                audioSource.clip = healSound;
                GetComponent<Skills>().Heal(audioSource);
            }
        }
        if (Input.GetKeyDown(jump))
        {
            audioSource.clip = jumpSound;
            if (Input.GetKey(down) && canDown)
            {
                GetComponent<Movement2D>().Down(anime, audioSource);
            }
            else 
                GetComponent<Movement2D>().Jump(anime, audioSource);
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
        if (PlayerPrefs.GetInt("CHARGESHOTLV") > 0)
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (anime.GetBool("isDying")) return;
        if (collision.TryGetComponent(out Wall w))
        {
            if (collision.transform.position.y > transform.position.y)
            {
                ceiling = true;
                if (w.canPass) collision.GetComponent<BoxCollider2D>().enabled = false;
            }  
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        string t = collision.gameObject.tag;
        if (t.Length > 5 && !isAlreadyHurt)
        {
            if (t.Substring(0, 5) == "patte" || t.Substring(0, 5) == "enemy" || t.Substring(0, 5) == "bosss"
                || t.Substring(0, 5) == "obsta")
            {
                isAlreadyHurt = true;
                StartCoroutine("HitColorAnimation");
                if (t.Substring(0, 5) == "patte") Destroy(collision.gameObject);
                playSound("Hit");
                GetComponent<PlayerLevel>().TakeDamage(float.Parse(t.Substring(5)), anime);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        ceiling = false;
        if (collision.TryGetComponent(out Wall w))
        {
           if (w.canPass) collision.GetComponent<BoxCollider2D>().enabled = true;
        }
        
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (anime.GetBool("isDying")) return;
        string t = collision.gameObject.tag;
        if (collision.gameObject.TryGetComponent(out Wall w) && GetComponent<Rigidbody2D>().velocity.y == 0)
            canDown = w.canPass;
        else
        {
            canDown = false;
            //if (t.Length > 5 && !isAlreadyHurt)
        }
    }


    IEnumerator HitColorAnimation()
    {
        GetComponent<SpriteRenderer>().color = Color.gray;
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().color = Color.white;
        isAlreadyHurt = false;
    }

    public void Key()
    {      
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("LEFT"), true);
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RIGHT"), true);
        jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JUMP"), true);
        down = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("DOWN"), true);
        attack = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ATTACK"), true);
        dash = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("DASH"), true);
        heal = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("HEAL"), true);
        action = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ACTION"), true);
    }

    void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, stage.LimitMin.x, stage.LimitMax.x),
                                         Mathf.Clamp(transform.position.y, stage.LimitMin.y-3, stage.LimitMax.y));
    }
}
