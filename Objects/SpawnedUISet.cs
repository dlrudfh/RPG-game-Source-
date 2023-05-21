using UnityEngine;
using UnityEngine.UI;

// ���� ü�� �� ��ġ ����
public class SpawnedUISet : MonoBehaviour
{
    private Vector3 distance = Vector3.up * 30.0f; // ���� ������Ʈ ��ܿ� ü�� �� ��ġ
    private Transform targetTransform;
    private RectTransform rectTransform;
    public void Setup(Transform target)
    {
        targetTransform = target; // �� Ʈ������ �� ����
        rectTransform = GetComponent<RectTransform>(); // ü�� ���� Ʈ������
    }
    private void LateUpdate()
    {
        // �� Ʈ�������� �������� ���� ��(���� óġ�Ǿ��� ��)
        if(targetTransform == null)
        {
            // ü�� �� ���� ����
            Destroy(gameObject);
            return;
        }
        // �� Ʈ�������� �̵� ��ο� �����ϰ� �̵���
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetTransform.position);
        rectTransform.position = screenPosition + distance;
    }
}
