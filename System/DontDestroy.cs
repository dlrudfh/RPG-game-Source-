using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//DontDestroy ������Ʈ�� ���� ��ũ��Ʈ
public class DontDestroy : MonoBehaviour
{
    // ���߿� �߰��� Dontdestroy������Ʈ�� ���� �迭 ���·� ������ ��
    [SerializeField] GameObject[] objects;
    public void Awake()
    {
        for(int x = 0; x < objects.Length; x++)
        {
            // �Էµ� ������Ʈ�鿡 dontdestroy�Ӽ� �ο�
            DontDestroyOnLoad(objects[x]);
        }
    }
}
