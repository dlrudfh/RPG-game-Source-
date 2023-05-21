using UnityEngine;

// ��Ϲ���
public class Saw : MonoBehaviour
{
    [SerializeField] float speed = 3; // ��� �̵��ӵ�
    // x, y��ǥ �̵�����
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minY;
    [SerializeField] float maxY;
    // x, y��ǥ �̵�����
    int dirX = 0;
    int dirY = 0;
    void Update()
    {
        // ���� ����, �ӵ� ���� ���� ���� �ݺ� �̵�
        transform.position += new Vector3(dirX, dirY, 0) * speed * Time.deltaTime;
        // x�� �̵� �ݰ��� 0�� �ƴ� ��
        if (maxX != minX)
        {
            // �¿� �ݺ� �̵�
            if (transform.position.x >= maxX) dirX = -1;
            else if (transform.position.x <= minX) dirX = 1;
        }
        // y�� �̵� �ݰ��� 0�� �ƴ� ��(else if�� �ƴϱ� ������ x, y��ǥ ���� �̵� ����)
        if (maxY != minY)
        {
            // ���� �ݺ� �̵�
            if (transform.position.y >= maxY) dirY = -1;
            else if (transform.position.y <= minY) dirY = 1;
        }
    }
}
