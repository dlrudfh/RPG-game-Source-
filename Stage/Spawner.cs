using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private StageData stage;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject notice;
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject enemyHp;
    [SerializeField]
    private float spawnTime;
    private int spawnCount;
    Transform walls;
    [SerializeField] bool spawn;
    [SerializeField] int spawnLimit = 20;

    private void Awake()
    {
        if (spawn)
        {
            walls = GameObject.Find("walls").transform;
            StartCoroutine("SpawnStart");
        }
        PlayerPrefs.SetInt("spawnCount", 0);
        GameObject p = Instantiate(player, new Vector3(PlayerPrefs.GetFloat("x"), PlayerPrefs.GetFloat("y"), 0), Quaternion.identity);
        //GameObject p = Instantiate(player, new Vector3(11.5f, 1, 0), Quaternion.identity); // 플레이어 위치 테스트용 임의설정
        p.GetComponent<Player>().stage = stage;
    }

    private IEnumerator SpawnStart()
    {
        yield return new WaitForSeconds(spawnTime);
        spawnCount = PlayerPrefs.GetInt("spawnCount");
        Transform floor = walls.GetChild(Random.Range(0, walls.childCount));
        while (true)
        {
            if(floor.TryGetComponent(out Wall w))
                if (w.canSpawn) break;
            floor = walls.GetChild(Random.Range(0, walls.childCount));
        }
        if (spawnCount <= spawnLimit)
        {
            spawnCount++;
            PlayerPrefs.SetInt("spawnCount", spawnCount);
            float x = Random.Range(floor.position.x - floor.localScale.x/2 + 0.5f, floor.position.x + floor.localScale.x/2 - 2.5f);
            float y = floor.position.y + 1; 
            Vector3 position = new Vector3(x, y, 1);
            GameObject enemyClone = Instantiate(enemyPrefab, position, Quaternion.identity);
            SpawnHp(enemyClone);
        }
        StopCoroutine("SpawnStart");
        StartCoroutine("SpawnStart");
    }

    private void SpawnHp(GameObject enemy)
    {
        GameObject sliderClone = Instantiate(enemyHp);
        sliderClone.transform.SetParent(GameObject.Find("System").transform);
        sliderClone.transform.localScale = Vector3.one;
        sliderClone.GetComponent<SpawnedUISet>().Setup(enemy.transform);
        sliderClone.GetComponent<EnemyHpViewer>().Setup(enemy.GetComponent<Enemy>());
    }
}
