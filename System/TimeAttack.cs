using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeAttack : MonoBehaviour
{
    float time;
    public float timeAttack = 10;
    GameObject darkness;
    public GameObject player;
    public Transform tilemap;
    public Transform startline;

    void Start()
    {
        darkness = GameObject.Find("emptyForDarkness").transform.GetChild(0).gameObject;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time < 1.5f) GetComponent<TextMeshProUGUI>().text = "Ready...";
        else if (time < 3) GetComponent<TextMeshProUGUI>().text = "Start!!!";
        else if (time < timeAttack + 3)
        {
            tilemap.GetComponent<TileChange>().ChangeTiles();
            GameObject.Find("walls").transform.Find("Timeattack").transform.Find("startline").gameObject.SetActive(false);
            player.GetComponent<Player>().enabled = true;
            darkness.SetActive(true);
            GetComponent<TextMeshProUGUI>().text = (timeAttack + 3 - Mathf.Ceil(time)).ToString();
        }
        else 
        {
            gameObject.SetActive(false);
            darkness.SetActive(false);
            startline.gameObject.SetActive(true);
            tilemap.GetComponent<TileChange>().RecoverTiles();
            player.transform.localPosition = new Vector3(-17, -2.5f, 0);
        }
            
    }

    void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        tilemap = GameObject.Find("Grid").transform.Find("Tilemap");
        startline = GameObject.Find("walls").transform.Find("Timeattack").transform.Find("startline");
        time = 0;
        player.GetComponent<Player>().enabled = false;
    }
}
