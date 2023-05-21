using UnityEngine;
using UnityEngine.SceneManagement;

// ��(��) ��ȯ ��ũ��Ʈ
// ���� ������ �̵��ϸ� �ڵ����� ���� ������ �Ѿ���� �ϴ� ��ũ��Ʈ
public class MapSwitch : MonoBehaviour
{
    [SerializeField] private string scene;
    [SerializeField] private float x;
    [SerializeField] private float y;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾ �� �̵� ������Ʈ�� �浹�Ͽ���,
        // ������ �� �� �̵��� �Ұ����� ���°� �ƴ� ��
        if (collision.gameObject.CompareTag("Player") && PlayerPrefs.GetInt("SwitchLock") == 0)
        {
            // �� ��ȯ �� ��ǥ ����
            SceneManager.LoadScene(scene);
            PlayerPrefs.SetFloat("x", x);
            PlayerPrefs.SetFloat("y", y);
        }
    }
}
