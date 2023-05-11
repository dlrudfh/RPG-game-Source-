using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            collision.GetComponent<Rigidbody2D>().velocity = Vector2.up * 15;
            collision.GetComponent<Movement2D>().jumpCount = 1;
            GetComponent<Animator>().Rebind();
            GetComponent<Animator>().Play("active");
        }
    }
}
