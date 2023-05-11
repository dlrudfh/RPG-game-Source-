using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] float speed = 3;
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minY;
    [SerializeField] float maxY;
    int dirX = 0;
    int dirY = 0;
    void Update()
    {
        transform.position += new Vector3(dirX, dirY, 0) * speed * Time.deltaTime;
        if (maxX != minX)
        {
            if (transform.position.x >= maxX) dirX = -1;
            else if (transform.position.x <= minX) dirX = 1;
        }
        if (maxY != minY)
        {
            if (transform.position.y >= maxY) dirY = -1;
            else if (transform.position.y <= minY) dirY = 1;
        }
    }
}
