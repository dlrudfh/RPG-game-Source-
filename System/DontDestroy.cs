using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    [SerializeField] GameObject[] objects;
    public void Awake()
    {
        for(int x = 0; x < objects.Length; x++)
        {
            DontDestroyOnLoad(objects[x]);
        }
    }
}
