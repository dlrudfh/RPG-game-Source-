using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darkness : MonoBehaviour
{
    void OnEnable()
    {   
        transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform);
        transform.localPosition = new Vector3(-0.18f, 0.08f, 0);
        transform.localScale = new Vector3(6, 6, 1);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}
