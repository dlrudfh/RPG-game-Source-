using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ΰ� ���Ǻη� ����Ǵ� ������Ʈ
public class ChangeAbleObstacle : MonoBehaviour
{
    [SerializeField] GameObject trigger;
    string t;
    void Start()
    {
        //���� �±״� 'obsta1', ���� ��� �Ұ���
        t = tag;
    }

    void Update()
    {
        if (trigger.GetComponent<Crank>().on)// ������������
        {
            GetComponent<BoxCollider2D>().isTrigger = true; // Ʈ���� ����
            tag = t;
        }
        else tag = "Untagged"; // �ѹ� ������ ������ ���Ŀ��� �±װ� ����Ǿ� ��ֹ� ������ �ƴ�
    }
}
