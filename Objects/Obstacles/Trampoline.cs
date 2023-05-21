using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ʈ���޸�(������)
public class Trampoline : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾ ������ �������� �浹�� ���(�¿� ���� �̵����� �浹�� ��쿡�� �������� ����)
        if (collision.CompareTag("Player") && collision.GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            // �÷��̾ ���� Ƣ�����
            collision.GetComponent<Rigidbody2D>().velocity = Vector2.up * 15;
            collision.GetComponent<Movement2D>().jumpCount = 1;
            // Ʈ���޸� �ִϸ��̼��� �� �� ����
            GetComponent<Animator>().Rebind();
            GetComponent<Animator>().Play("active");
        }
    }
}
