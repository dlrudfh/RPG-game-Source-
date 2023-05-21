using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� ������Ʈ
public class Wall : MonoBehaviour
{
    public bool canPass = true; // true�� ��� �������� ������ ��
    public bool canSpawn = true; // true�� ��� ���� ���� �������� ������ ������ ��

    void OnCollisionStay2D(Collision2D collision)
    {
        //�÷��̾ ���������� �� ���
        if (collision.gameObject.CompareTag("Player") && PlayerPrefs.GetInt("FALL") == 1)
        {
            // �ش� �� ������Ʈ�� �ݶ��̴��� ��Ȱ��ȭ�� �÷��̾ ���
            GetComponent<BoxCollider2D>().enabled = false;
            // �÷��̾��� �������� �ɼ��� ����
            PlayerPrefs.SetInt("FALL", 0);
        }
    }

}
