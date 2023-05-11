using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crank : MonoBehaviour
{
    [SerializeField] Sprite down;
    [SerializeField] Sprite up;
    public bool on = false;
    bool nearby;
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.8f, 1);
            nearby = true;
        }
    }

    void Update()
    {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("EFFECT");
        if (Input.GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ACTION"), true)) && nearby)
        {
            GetComponent<AudioSource>().Play();
            if (on)
            {
                GetComponent<SpriteRenderer>().sprite = up;
                GetComponent<TileChange>().RecoverTiles();
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = down;
                GetComponent<TileChange>().ChangeTiles();
            }
            on = !on;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            nearby = false;
        }
    }
}
