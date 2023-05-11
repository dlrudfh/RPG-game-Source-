using UnityEngine;
using System.Collections;

public class ObjectChaser : MonoBehaviour
{
    [SerializeField]
    private StageData stage;
    private Vector2 velocity;
    public GameObject player;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MUSIC");
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, 0);
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, 0);
        transform.position = new Vector3(posX, posY, transform.position.z);
    }
    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, stage.LimitMin.x + 9f, stage.LimitMax.x - 9f),
                                         Mathf.Clamp(transform.position.y, stage.LimitMin.y+4, stage.LimitMax.y-5), transform.position.z);
    }
}