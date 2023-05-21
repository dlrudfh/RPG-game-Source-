using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Ʒ��� ������� ������ ������Ʈ
public class Rockhead : MonoBehaviour
{
    GameObject player;
    float speed = 10; // �������� �ӵ�
    int fall = 0; // �������� ����(0 = ����, 1 = �ϰ�, 2 = ���)
    [SerializeField] float maxY; // �ִ� ����(���� ��ġ)
    [SerializeField] float minY; // �ּ� ����(�ٴ�)
    
    void Start()
    {   
        // �÷��̾� ������Ʈ Ž��
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (transform.position.y <= minY) fall = 2; // �ٴڿ� ���� ��� ��� ����
        else if (transform.position.y >= maxY) // ���� ��ġ�� ���� �Ϸ����� ���
        {   
            // �÷��̾ ������Ʈ �ϴ��� �νĹ��� ���� ������ ���
            if (player.transform.position.y <= transform.position.y &&
            player.transform.position.x >= transform.position.x - 0.5f &&
            player.transform.position.x <= transform.position.x + 0.5f) 
                fall = 1; // �ϰ�����
            else fall = 0; // ����(������Ʈ�� ���� ��ġ���� �ö��� �ʵ��� �ϱ� ����)
        }
        // �ϴܿ� �÷��̾� ���� �� �ް��ϰ� �ϰ�
        if (fall==1) transform.position += new Vector3(0, -1, 0) * speed * Time.deltaTime;
        // fall ������ 2�� �� õõ�� ���
        else if (fall==2) transform.position += new Vector3(0, 1, 0) * 1 * Time.deltaTime;
    }
}
