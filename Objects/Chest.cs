using UnityEngine;

// ���� ������Ʈ
public class Chest : MonoBehaviour
{
    bool nearby;
    bool open;
    [SerializeField] int reward;
    [SerializeField] Sprite img;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.8f, 1);
            nearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            nearby = false;
        }
    }

    private void Update()
    {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("EFFECT");
        // ������ ���� ���� & �÷��̾� ���� & �׼� Ű Ȱ��ȭ
        if (!open && nearby && Input.GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ACTION"), true)))
        {
            open = true;
            // �ý��� ���� â���� ���� ���� ȹ�� �޽����� �����
            PlayerPrefs.SetString("Notice", "You got " + reward + "G!");
            // ���� ���� ȿ���� ���
            GetComponent<AudioSource>().Play();
            // ���� ȹ��
            PlayerPrefs.SetInt("GOLD", PlayerPrefs.GetInt("GOLD") + reward);
            // �����ִ� ���� �̹����� ����
            GetComponent<SpriteRenderer>().sprite = img;
        }
    }
}
