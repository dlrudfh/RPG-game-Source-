using UnityEngine;
using UnityEngine.SceneManagement;

// ��Ż(�� �̵�)
public class Portal : MonoBehaviour
{
    [SerializeField] private string scene; // �̵��� ��
    [SerializeField] private float x; // ���� ������ �÷��̾ ������ x��ǥ
    [SerializeField] private float y;// ���� ������ �÷��̾ ������ y��ǥ
    bool nearby; // �÷��̾ ������ �ִ���
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�÷��̾ ����� ��� ��⸦ ������ ǥ��
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.8f, 1);
            nearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //�÷��̾ �־��� ��� ��⸦ �ʱ�ȭ
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            nearby = false;
        }
    }

    private void Update()
    {
        // ��Ż�� Ȱ��ȭ ������ ����Ʈ ������ �����
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("EFFECT");
        // ��Ż�� ������ �׼� ��ư�� ������ ��
        if (Input.GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ACTION"), true)) && nearby)
        {
            //�� �ε� �� ��ǥ ����, ȿ���� ���
            SceneManager.LoadScene(scene);
            PlayerPrefs.SetFloat("x", x);
            PlayerPrefs.SetFloat("y", y);
            GetComponent<AudioSource>().Play();
            nearby = false;
        }
    }
}
