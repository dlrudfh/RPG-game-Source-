using UnityEngine;

public class QuestTab : MonoBehaviour
{
    public int n;

    public void Description()
    {
        // ����Ʈ ���� â�� ��� ����Ʈ ������ ������ �� ��
        // Ŭ���� ����Ʈ ��ȣ ���� ���ڷ� �Ѱ������ν� �ش� ��ȣ�� ����Ʈ ������ ��µǵ��� ��
        transform.parent.GetComponent<Quest>().Description(n);
    }
}
