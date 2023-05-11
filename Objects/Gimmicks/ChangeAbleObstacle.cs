using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAbleObstacle : MonoBehaviour
{
    [SerializeField] GameObject trigger;
    string t;
    void Start()
    {
        t = tag;
    }

    void Update()
    {
        if (trigger.GetComponent<Crank>().on)//������������
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
            tag = t;
        }
        else tag = "Untagged";
    }
}
