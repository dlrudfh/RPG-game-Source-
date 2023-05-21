using UnityEngine;
using System.Collections;

// �� ������Ʈ
public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private  float maxHp;
    [SerializeField]
    private  float curHp;
    [SerializeField]
    private GameObject coin;
    [SerializeField]
    private GameObject money;
    [SerializeField]
    private float time; // ���� �ð����� �̵� ���� ����
    private int direction = 1; // �̵� ����
    public Animator anime; // ��� �ִϸ��̼�
    [SerializeField] private float exp = 1; // óġ�� ȹ���ϴ� ����ġ

    public float MaxHp => maxHp;
    public float CurHp => curHp;

    private void Awake()
    {
        curHp = maxHp;
    }

    private void Update()
    {
        // �̹� ����� ���¶�� �������� ����
        if (anime.GetBool("Die")) return;
        // �¿�� �����̸� ���� �ð����� ������ ��ȯ�ϴ� �ݺ��
        time += Time.deltaTime;
        transform.position += new Vector3(direction, 0, 0) * 1.0f * Time.deltaTime;
        if (time > 2 )
        {
            direction = direction * (-1);
            time = time - 2;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {  
        if (anime.GetBool("Die")) return;
        // �Ѿ˰� ���� ��
        if (collision.CompareTag("bullet") || collision.CompareTag("chargedBullet"))
        {   
            // �Ѿ� Ȥ�� ���� ������ ������ ��ŭ ������Ʈ�� ü�� ����
            if (collision.CompareTag("bullet")) Destroy(collision.gameObject);
            if (collision.CompareTag("bullet")) curHp -= PlayerPrefs.GetFloat("DMG");
            else curHp -= PlayerPrefs.GetFloat("DMG") * (1 + PlayerPrefs.GetInt("CHARGESHOT")*0.4f);
            // �ǰ� �ִϸ��̼� ����
            StopCoroutine("HitColorAnimation");
            StartCoroutine("HitColorAnimation");
            // ü���� 0������ ��
            if (curHp <= 0)
            {
                // �ݶ��̴� ��Ȱ��ȭ(óġ�Ǿ������� �÷��̾�� �浹�Ͽ� �������� ������ ������ ���ֱ� ����)
                GetComponent<BoxCollider2D>().enabled = false;
                // ����ġ ȹ��
                player.GetComponent<PlayerLevel>().GetExp(exp);
                // ��� �ִϸ��̼� ����
                anime.SetBool("Die", true);
                // ������ �� ����
                int spawnCount = PlayerPrefs.GetInt("spawnCount") - 1;
                PlayerPrefs.SetInt("spawnCount", spawnCount);
                // ��� �̺�Ʈ
                Invoke("DropItem", 1.0f);
            }
        }
    }

    private void DropItem()
    {
        GameObject gold;
        // ���� Ȯ���� ������ ���ٹ��� ���� ���
        int rand = Random.Range(1, 11);
        if (rand <= 5) gold = Instantiate(money, transform.position, Quaternion.identity);
        else gold = Instantiate(coin, transform.position, Quaternion.identity);
        gold.GetComponent<Gold>().Drop();
        // ����Ʈ ���� �˻� �� ����
        if (gameObject.tag == "enemy1")
        {
            QuestSummary.progress[1]++;
        }
        // ������Ʈ ����
        Destroy(gameObject);
    }

    private IEnumerator HitColorAnimation()
    { 
        // �� ������Ʈ�� �ǰݴ��� �� ��� ���� �����
        GetComponent<SpriteRenderer>().color = Color.gray;
        yield return new WaitForSeconds(0.05f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
