using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 5.0f; // �̵��ӵ�
    public StageData stage; // �������� ����(�÷��̾� �̵� ���� �ݰ�)
    [SerializeField] GameObject bulletPrefab; // �Ѿ�
    [SerializeField] GameObject chargedBullet; // ��������
    //�÷��̾� ����Ű
    [SerializeField] KeyCode left;
    [SerializeField] KeyCode right;
    [SerializeField] KeyCode attack;
    [SerializeField] KeyCode jump;
    [SerializeField] KeyCode down;
    [SerializeField] KeyCode dash;
    [SerializeField] KeyCode heal;
    [SerializeField] KeyCode action;
    //

    [SerializeField] bool canDown; // ���������� ���� ����
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
    int dir; // ����
    float startTime; // ���� �ð� ������ ���� ����
    bool chargeComplete = true; // ���� �Ϸ� ����
    bool isAlreadyHurt = false; // �ǰ������� ����(���� �ǰ� ����)
    bool ceiling; // õ�忡 ���������� ����

    void Start()
    {
        dir = 1;
        audioSource = GetComponent<AudioSource>();
        // Ű���� �Լ� ����
        Key();
    }

    // ���ϴ� ȿ������ ����ϴ� �Լ�
    void playSound(string cond)
    {
        // �Է¹��� ���ڿ��� �ش��ϴ� ȿ������ clip�� �Ҵ�
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
        // ȿ���� ���
        audioSource.Play();
    }

    void Update()
    {
        // �÷��̾� ȿ���� ���� ����
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("EFFECT");
        // �÷��̾ ������� ��� �������� ����
        if (anime.GetBool("isDying")) return;
        // �÷��̾ �ϰ����� ��
        if (GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            // ���� Ƚ���� �ּ� 1ȸ�� ����
            GetComponent<Movement2D>().jumpCount = GetComponent<Movement2D>().jumpCount == 2 ? 2 : 1;
            // �ϰ� ���·� ����(�ִϸ��̼� ���)
            anime.SetBool("isJumping", false);
            anime.SetBool("isFalling", true);
        }
        // �÷��̾ y���̵��� ���� ��(�ٴڿ� ��� ���� ��)
        else if (GetComponent<Rigidbody2D>().velocity.y == 0)
        {
            // �÷��̾ �ٴڿ� ������ ���� Ƚ���� �ʱ�ȭ ���־�� �ϴµ�
            // ��� ���̰ų� õ�忡 �ε����� ���� ���������� y�� �̵��ӵ��� 0�� �Ǳ� ������
            // �ش� ������ ��� �˻���
            if (!ceiling && !GetComponent<Movement2D>().doNotDash)
            {
                GetComponent<Movement2D>().jumpCount = 0;
            }
            // �ִϸ��̼� ����(idle���·� ���ư�)
            anime.SetBool("isFalling", false);
            anime.SetBool("isJumping", false);
        }
        // ������� ���(����)
        else anime.SetBool("isFalling", false);
        // ��� ��ų�� ��� ������ ��
        if (PlayerPrefs.GetInt("DASHLV") > 0)
        {
            //��� Ű�� �Է�
            if (Input.GetKeyDown(dash))
            {
                //ȿ������ ��÷� ����
                audioSource.clip = dashSound;
                // ��� �۵�
                GetComponent<Movement2D>().Dash(dir, anime, audioSource);
                anime.SetBool("isFalling", true);
            }
        }
        // �� ��ų�� ��� ������ ��
        if (PlayerPrefs.GetInt("HEALLV") > 0)
        {
            //�� Ű�� �Է�
            if (Input.GetKeyDown(heal))
            {
                // ȿ������ ���� ����
                audioSource.clip = healSound;
                //�� �۵�
                GetComponent<Skills>().Heal(audioSource);
            }
        }
        // ���� Ű�� �Է�
        if (Input.GetKeyDown(jump))
        {
            //ȿ������ ������ ����
            audioSource.clip = jumpSound;
            // ���� Ű�� ������ �� �Ʒ����� Ű�� ���� �ԷµǾ� �ִ� ���¿��� ���(��������)
            if (Input.GetKey(down) && canDown)
            {
                //�������� �۵�
                GetComponent<Movement2D>().Down(anime, audioSource);
            }
            // ���� �۵�
            else GetComponent<Movement2D>().Jump(anime, audioSource); 
        }
        // �¿� ����Ű
        bool l = Input.GetKey(left);
        bool r = Input.GetKey(right);
        // �¿� ����Ű �� ��� ���� �ԷµǴ��� �޸��� ���·� �����
        anime.SetBool("isRunning", l || r);
        // �̵� ���⿡ ���� ���� ���� �� ����
        if (l) dir = -1; else if (r) dir = 1;
        // �̵� ���⿡ ���� �÷��̾� ���� ȸ��
        transform.rotation = Quaternion.Euler(0, 90-90*dir, 0);
        // �÷��̾� �̵�
        transform.position += new Vector3(anime.GetBool("isRunning") ? dir : 0, 0, 0) * speed * Time.deltaTime;
        // ���� Ű�� �Է�
        if (Input.GetKeyDown(attack))
        {
            playSound("Shoot");
            // �Է� ������ �ð� ���
            startTime = Time.time;
            // �Է��� ���� ���������Ƿ� �������� ����
            chargeComplete = false;
            // �Ѿ� ������Ʈ ���� �� ĳ���Ͱ� �Ĵٺ��� �������� ���ư�
            GameObject bullet = Instantiate(bulletPrefab, new Vector3(transform.position.x + 0.5f*dir, transform.position.y, 0), Quaternion.identity);
            bullet.GetComponent<Movement2D>().Setup(10.0f, new Vector3(dir, 0, 0));
            Destroy(bullet, 2.0f);
        }
        // ���� ���� ��ų�� ��� ������ ��
        if (PlayerPrefs.GetInt("CHARGESHOTLV") > 0)
        {
            // ������ �Ϸ��ߴٸ�(0.7�� ���)
            if(chargeComplete == false && Time.time - startTime >= 0.7f)
            {
                // ���� �Ϸ� ȿ���� ���
                playSound("ChargeComplete");
                chargeComplete = true;
            }
            // ���� Ű�� ���� ��
            if (Input.GetKeyUp(attack))
            {
                // ������ �Ϸ�� ���¶��
                if (chargeComplete == true)
                {
                    // ���� ���� ����
                    playSound("ChargeShoot");
                    GameObject charge = Instantiate(chargedBullet, new Vector3(transform.position.x + 0.5f * dir, transform.position.y, 0), Quaternion.identity);
                    charge.GetComponent<Movement2D>().Setup(20.0f, new Vector3(dir, 0, 0));
                    Destroy(charge, 1.0f);
                }
                else chargeComplete = true;
            }
        }
    }

    //�÷��̾�Դ� �÷��̾� ũ���� circle collider�� �÷��̾� �Ӹ��� ���� box collider�� ������.
    // circle collider�� ����� �� ���� �ݶ��̴���, ���̳� ���� �ε����� ���ǿ� �����ϵ��� ���ִ� ������ ��.
    // box collider�� istriggerŸ���� �ݶ��̴���, �� ������Ʈ�� ��� ���θ� �������ִ� ������ ��.

    void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾ ����ߴٸ� �۵����� ����
        if (anime.GetBool("isDying")) return;
        // �Ӹ��� �浹�� ������Ʈ�� ������ Ȯ��
        if (collision.TryGetComponent(out Wall w))
        {
            // �浹�� ������Ʈ�� �÷��̾�� ���� �ִٸ�
            if (collision.transform.position.y > transform.position.y)
            {
                //õ�� ���� = true(y�� �ӵ��� 0�̶� ���� Ƚ���� �ʱ�ȭ���� ����)
                ceiling = true;
                // �ش� ������Ʈ�� ����� ������ ���̶�� ������Ʈ�� collider�� ��Ȱ��ȭ�Ͽ� ���
                if (w.canPass) collision.GetComponent<BoxCollider2D>().enabled = false;
            }  
        }
    }

    //triggerstay�̱� ������ �� ������Ʈ�� ���������� ��� ���� ��� ���� �ð����� ��� ���ظ� ���� �� �ִ�.
    void OnTriggerStay2D(Collider2D collision)
    {
        // ������Ʈ�� Ÿ���� Ȯ���ϱ� ���� �±� ���� ����
        string t = collision.gameObject.tag;
        // �ǰ� ���� ���°� �ƴ� ��
        if (t.Length > 5 && !isAlreadyHurt)
        {
            // �浹�� ������Ʈ�� ����, ��, ����, ��ֹ��� ���
            if (t.Substring(0, 5) == "patte" || t.Substring(0, 5) == "enemy" || t.Substring(0, 5) == "bosss"
                || t.Substring(0, 5) == "obsta")
            {
                // �ǰݴ������Ƿ� ��õ��� �ǰ� ���� ó��
                isAlreadyHurt = true;
                // �ǰ� �ִϸ��̼� ���
                StartCoroutine("HitColorAnimation");
                // ������ ��� �ǰ� �� ������ ������
                if (t.Substring(0, 5) == "patte") Destroy(collision.gameObject);
                playSound("Hit");
                // �ش� ������Ʈ�� �±� ���� ���� �ִ� ���ڸ�ŭ ���ظ� ����
                GetComponent<PlayerLevel>().TakeDamage(float.Parse(t.Substring(5)), anime);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // �浹�� ����Ǿ����Ƿ� õ�忡 ��� ���� ���� ���� ������ ceiling = false
        ceiling = false;
        // �浹�� ����� ������Ʈ�� ������ Ȯ��
        if (collision.TryGetComponent(out Wall w))
        {
            // ��� ������ ���̾��� ��� ���������� ���� �÷��̾ ���� ��ġ�� �ʰ� �Ǹ� 
            // ���� �ݶ��̴��� ��Ȱ��ȭ��(Ȱ��ȭ���� ������ ���� �ѹ� ����� ���Ŀ� �ٽô� ���� ������ �� ���� ����)
            if (w.canPass) collision.GetComponent<BoxCollider2D>().enabled = true;
        }
           
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        // �÷��̾ ����ߴٸ� �۵����� ����
        if (anime.GetBool("isDying")) return;
        // �浹�� ������Ʈ�� ������ Ȯ���� �� �´ٸ� canPass ���� ���� canDown�� ����
        // (���� ��� ������(canPass) ������ ��� ���� ������ �����ϰ�(canDown), �ݴ��� ��� ���� ���� �Ұ���)
        if (collision.gameObject.TryGetComponent(out Wall w) && GetComponent<Rigidbody2D>().velocity.y == 0)
            canDown = w.canPass;
        else canDown = false;
    }

    //�ǰ� �ִϸ��̼�
    IEnumerator HitColorAnimation()
    {
        // �ǰ� �� 0.5�ʰ� �ǰ� ���� ������ �־����� ĳ���Ͱ� �����
        GetComponent<SpriteRenderer>().color = Color.gray;
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().color = Color.white;
        isAlreadyHurt = false;
    }

    public void Key()
    {      
        // PlayerPrefs�� ����� ���ڿ� ������ �÷��̾� ����Ű ���� ������ ��
        // KeyCode���·� �Ľ��Ͽ� �˸��� ������ ���� ������ ��
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
