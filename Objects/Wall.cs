using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public bool canPass = true;
    public bool canSpawn = true;

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && PlayerPrefs.GetInt("FALL") == 1)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            PlayerPrefs.SetInt("FALL", 0);
        }
    }

}
