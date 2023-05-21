using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// ���� ������Ʈ
public class Boss1 : MonoBehaviour
{
    [SerializeField] GameObject player; 
    [SerializeField] GameObject pattern;
    public float maxHp;
    public float curHp;
    [SerializeField] GameObject coin;
    [SerializeField] GameObject money;
    [SerializeField] float time;
    int direction = 1; // ���Ʒ� �ݺ���� ���� ���⺯��
    [SerializeField] float exp = 10; // ���� ���ް���ġ
    [SerializeField] Animator anime;
    [SerializeField] private Slider hpSlider;
    float speed = 3;
    bool berserk;

    public float MaxHp => maxHp;
    public float CurHp => curHp;

    private void Awake()
    {
        curHp = maxHp;
        // ���̵� ����
        PlayerPrefs.SetInt("SwitchLock", 1);
        // ���� �ڷ�ƾ ����
        StartCoroutine(Pattern(0.1f));
    }

    private void Update()
    {
        // ���� ������ �� ���ǵ� = 5
        if (berserk) speed = 5;
        // ����� ���¶�� ���� ����
        if (anime.GetBool("Die")) return;
        // ü�� �����̴� ���� ���� ü�� ������ �ݿ�
        hpSlider.value = curHp/maxHp;
        // �� �Ʒ��� ���� �ð����� ������ �ٲٸ� �ݺ� �
        time += Time.deltaTime;
        transform.position += new Vector3(0, direction, 0) * speed * Time.deltaTime;
        if (time > 9/speed)
        {
            direction = direction * (-1);
            time = time - 9/speed;
            // ü���� 30% �̸��� �� ����ȭ
            if (curHp / maxHp < 0.3f) berserk = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (anime.GetBool("Die")) return;
        // �Ѿ˰� �浹 ��
        if (collision.CompareTag("bullet") || collision.CompareTag("chargedBullet"))
        {
            // �������� ����
            if (collision.CompareTag("bullet")) Destroy(collision.gameObject);
            if (collision.CompareTag("bullet")) curHp -= PlayerPrefs.GetFloat("DMG");
            else curHp -= PlayerPrefs.GetFloat("DMG") * (1 + PlayerPrefs.GetInt("CHARGESHOT") * 0.4f);
            // �浹 �ִϸ��̼�
            StopCoroutine("HitColorAnimation");
            StartCoroutine("HitColorAnimation");
            if (curHp <= 0)
            {
                hpSlider.value = 0;
                player.GetComponent<PlayerLevel>().GetExp(exp);
                anime.SetBool("Die", true);
                Invoke("DropItem", 1.0f);
            }
        }
    }

    // ���� óġ �� �������� �����
    private void DropItem()
    {
        GameObject gold;
        // ���� ���� ������ ���� Ȯ���� ���ٹ߰� ������ �����
        int rand = Random.Range(1, 6);
        if (rand <= 3) gold = Instantiate(money, transform.position, Quaternion.identity);
        else gold = Instantiate(coin, transform.position, Quaternion.identity);
        gold.GetComponent<Gold>().Drop();
        // ���� óġ �� �� �̵� ��� Ȱ��ȭ
        PlayerPrefs.SetInt("SwitchLock", 0);
        //��� �Ϸ� �� ������ ü�� �� ����
        Destroy(hpSlider.gameObject);
        Destroy(gameObject);
    }

    // �ǰ� �ִϸ��̼�
    private IEnumerator HitColorAnimation()
    { 
        // ������ ��� ������� �ǰݵǾ����� ǥ����
        GetComponent<SpriteRenderer>().color = Color.gray;
        yield return new WaitForSeconds(0.05f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    // ���� ����
    private IEnumerator Pattern(float delay)
    {
        // ���� ����
        GameObject pat = Instantiate(pattern, new Vector3(7, transform.position.y, 0), Quaternion.identity);
        // ������ �������� ���ư�
        pat.GetComponent<Movement2D>().Setup(7.0f, Vector3.left); 
        yield return new WaitForSeconds(delay);
        // ���� �ð����� ���� �ݺ�(����ȭ ������ ��� ���� �ݺ� �ֱⰡ ª����)
        StartCoroutine(Pattern(berserk?0.3f:0.6f));
    }
}
