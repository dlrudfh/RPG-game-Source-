using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Chest : MonoBehaviour
{
    bool nearby;
    bool open;
    [SerializeField] int reward;
    [SerializeField] Sprite img;

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
        if (!open && nearby && Input.GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ACTION"), true)))
        {
            open = true;
            GameObject.Find("System").transform.Find("Notice").GetComponent<Notice>().message("You got " + reward + "G!");
            GetComponent<AudioSource>().Play();
            PlayerPrefs.SetInt("GOLD", PlayerPrefs.GetInt("GOLD") + reward);
            GetComponent<SpriteRenderer>().sprite = img;
        }
    }
}
