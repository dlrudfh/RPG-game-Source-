using UnityEngine;
using UnityEngine.UI;

public class SpawnedUISet : MonoBehaviour
{
    private Vector3 distance = Vector3.up * 30.0f;
    private Transform targetTransform;
    private RectTransform rectTransform;
    public void Setup(Transform target)
    {
        targetTransform = target;
        rectTransform = GetComponent<RectTransform>();
    }
    private void LateUpdate()
    {
        if(targetTransform == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetTransform.position);
        rectTransform.position = screenPosition + distance;
    }
}
