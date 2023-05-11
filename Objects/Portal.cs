using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private string scene;
    [SerializeField] private float x;
    [SerializeField] private float y;
    bool nearby;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.8f, 1);
            nearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            nearby = false;
        }
    }

    private void Update()
    {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("EFFECT");
        if (Input.GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ACTION"), true)) && nearby)
        {
            SceneManager.LoadScene(scene);
            PlayerPrefs.SetFloat("x", x);
            PlayerPrefs.SetFloat("y", y);
            GetComponent<AudioSource>().Play();
            nearby = false;
        }
    }
}
