using UnityEngine;

public class QuestTab : MonoBehaviour
{
    public int n;

    public void Description()
    {
        transform.parent.GetComponent<Quest>().Description(n);
    }
}
