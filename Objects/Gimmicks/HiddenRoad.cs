using UnityEngine;

public class HiddenRoad : MonoBehaviour
{
    GameObject chest;
    void Start()
    {
        chest = transform.Find("Chest").gameObject;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<TileChange>().ChangeTiles();
            chest.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<TileChange>().RecoverTiles();
            chest.SetActive(false);
        }
    }
}
