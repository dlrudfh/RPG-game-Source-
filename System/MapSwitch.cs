using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSwitch : MonoBehaviour
{
    [SerializeField] private string scene;
    [SerializeField] private float x;
    [SerializeField] private float y;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(scene);
            PlayerPrefs.SetFloat("x", x);
            PlayerPrefs.SetFloat("y", y);
        }
    }
}
