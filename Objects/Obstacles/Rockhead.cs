using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rockhead : MonoBehaviour
{
    GameObject player;
    float speed = 10;
    int fall = 0;
    [SerializeField] float maxY;
    [SerializeField] float minY;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (transform.position.y <= minY) fall = 2;
        else if (transform.position.y >= maxY)
        {
            if (player.transform.position.y <= transform.position.y &&
            player.transform.position.x >= transform.position.x - 0.5f &&
            player.transform.position.x <= transform.position.x + 0.5f) fall = 1;
            else fall = 0;
        }
        if (fall==1) transform.position += new Vector3(0, -1, 0) * speed * Time.deltaTime;
        else if (fall==2) transform.position += new Vector3(0, 1, 0) * 1 * Time.deltaTime;
    }
}
