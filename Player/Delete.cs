using UnityEngine;

public class Delete : MonoBehaviour
{
    [SerializeField]
    private StageData stage;
    void Update()
    {
        if(transform.position.x > stage.LimitMax.x || transform.position.x < stage.LimitMin.x || transform.position.y > stage.LimitMax.y || transform.position.y < stage.LimitMin.y)
        {
            Destroy(gameObject);
        }
    }
}
