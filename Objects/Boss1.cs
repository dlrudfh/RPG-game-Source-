using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Boss1 : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject pattern;
    public float maxHp;
    public float curHp;
    [SerializeField] GameObject coin;
    [SerializeField] GameObject money;
    [SerializeField]float time;
    int direction = 1;
    [SerializeField] float exp = 10;
    [SerializeField] Animator anime;
    [SerializeField] private Slider hpSlider;
    float speed = 3;
    bool berserk;

    public float MaxHp => maxHp;
    public float CurHp => curHp;

    private void Awake()
    {
        curHp = maxHp;
        StartCoroutine(Pattern(0.1f));
    }

    private void Update()
    {
        if (berserk) speed = 5;
        if (anime.GetBool("Die")) return;
        hpSlider.value = curHp/maxHp;
        time += Time.deltaTime;
        transform.position += new Vector3(0, direction, 0) * speed * Time.deltaTime;
        if (time > 9/speed)
        {
            direction = direction * (-1);
            time = time - 9/speed;
            if (curHp / maxHp < 0.3f) berserk = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (anime.GetBool("Die")) return;
        if (collision.CompareTag("bullet") || collision.CompareTag("chargedBullet"))
        {
            if (collision.CompareTag("bullet")) Destroy(collision.gameObject);
            if (collision.CompareTag("bullet")) curHp -= PlayerPrefs.GetFloat("DMG");
            else curHp -= PlayerPrefs.GetFloat("DMG") * (1 + PlayerPrefs.GetInt("CHARGESHOT") * 0.4f);
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

    private void DropItem()
    {
        GameObject gold;
        int rand = Random.Range(1, 11);
        if (rand <= 5) gold = Instantiate(money, transform.position, Quaternion.identity);
        else gold = Instantiate(coin, transform.position, Quaternion.identity);
        gold.GetComponent<Gold>().Drop();
        Destroy(hpSlider.gameObject);
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

    private IEnumerator Pattern(float delay)
    {
        GameObject pat = Instantiate(pattern, new Vector3(7, transform.position.y, 0), Quaternion.identity);
        pat.GetComponent<Movement2D>().Setup(7.0f, Vector3.left);
        yield return new WaitForSeconds(delay);
        StartCoroutine(Pattern(berserk?0.3f:0.6f));
    }
}
