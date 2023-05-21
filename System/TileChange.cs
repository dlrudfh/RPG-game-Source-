using UnityEngine;
using UnityEngine.Tilemaps;

//Ÿ�� ���� ��ũ��Ʈ
public class TileChange : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] int minX = 0;
    [SerializeField] int maxX = 0;
    [SerializeField] int minY = 0;
    [SerializeField] int maxY = 0;
    [SerializeField] Vector3Int[] vector; // Ÿ���� ������ ��ġ ���� ����
    [SerializeField] TileBase[] t1; // ������ Ÿ�� ����
    [SerializeField] TileBase[] t2; // ���� Ÿ�� ����
    
    void Start()
    {
        int i = 0;
        if (minX <= maxX && minY <= maxY)
        {
            for (int y = maxY; y >= minY; y--)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    //inspector���� ������ ���� ���� �迭�� ����
                    vector[i++] = new Vector3Int(x, y, 0);
                }
            }
        }
    }

    public void ChangeTiles()
    {
        //Ÿ�� ����
        tilemap.SetTiles(vector, t1);
    }

    public void RecoverTiles()
    {
        //Ÿ�� ����
        tilemap.SetTiles(vector, t2);
    }
}
