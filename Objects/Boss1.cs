using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Boss1 : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private  int maxHp;
    [SerializeField]
    private  int curHp;
    [SerializeField]
    private GameObject coin;
    [SerializeField]
    private GameObject money;
    [SerializeField]
    private float time;
    private int direction = 1;
    [SerializeField] int exp = 10;
    [SerializeField] Animator anime;
    [SerializeField] private Slider hpSlider;

    public int MaxHp => maxHp;
    public int CurHp => curHp;

    private void Awake()
    {
        curHp = maxHp;
    }

    private void Update()
    {
        if (anime.GetBool("Die")) return;
        hpSlider.value = (float)curHp / (float)maxHp;
        time += Time.deltaTime;
        transform.position += new Vector3(0, direction, 0) * 3.0f * Time.deltaTime;
        if (time > 2.8f )
        {
            direction = direction * (-1);
            time = time - 2.8f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (anime.GetBool("Die")) return;
        if (collision.CompareTag("bullet") || collision.CompareTag("chargedBullet"))
        {
            if (collision.CompareTag("bullet")) Destroy(collision.gameObject);
            if (collision.CompareTag("bullet")) curHp -= PlayerPrefs.GetInt("DMG");
            else {
                curHp -= (int)(PlayerPrefs.GetInt("DMG") * (1 + PlayerPrefs.GetInt("CHARGESHOT")*0.4f));
                Debug.Log(PlayerPrefs.GetInt("DMG") * (1 + PlayerPrefs.GetInt("CHARGESHOT") * 0.4f));
                Debug.Log((int)(PlayerPrefs.GetInt("DMG") * (1 + PlayerPrefs.GetInt("CHARGESHOT") * 0.4f)));
            }
            StopCoroutine("HitColorAnimation");
            StartCoroutine("HitColorAnimation");
            if (curHp <= 0)
            {
                player.GetComponent<PlayerLevel>().GetExp(exp);
                anime.SetBool("Die", true);
                Invoke("DropItem", 1.0f);
            }
        }
    }

    private void DropItem()
    {
        GameObject gold;
        int rand = Random.Range(1, 11);
        if (rand <= 5) gold = Instantiate(money, transform.position, Quaternion.identity);
        else gold = Instantiate(coin, transform.position, Quaternion.identity);
        gold.GetComponent<Gold>().Drop();
        Destroy(gameObject);
    }

    private IEnumerator HitColorAnimation()
    { 
        GetComponent<SpriteRenderer>().color = Color.gray;
        yield return new WaitForSeconds(0.05f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private float HP()
    {
        return curHp / maxHp;
    }
}