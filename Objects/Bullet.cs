using UnityEngine;

//�Ѿ� ������Ʈ
public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �Ѿ��� ���� ������ ������
        if (collision.gameObject.CompareTag("wall"))
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // �Ѿ��� ������ ���ư��� �ʰ� ���� 1�� �� ������
        Destroy(gameObject, 1);
    }
}
