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

    private void Awake()
    {
        if(enemyPrefab!=null)StartCoroutine("SpawnStart");
        PlayerPrefs.SetInt("spawnCount", 0);
        Instantiate(player, new Vector3(PlayerPrefs.GetFloat("x"), PlayerPrefs.GetFloat("y"), 0), Quaternion.identity);
    }

    private IEnumerator SpawnStart()
    {
        spawnCount = PlayerPrefs.GetInt("spawnCount");
        if (spawnCount <= 10)
        {
            spawnCount++;
            PlayerPrefs.SetInt("spawnCount", spawnCount);
            float x = Random.Range(stage.LimitMin.x, stage.LimitMax.x);
            float y = Random.Range(stage.LimitMin.y + 1.5f, stage.LimitMax.y);
            Vector3 position = new Vector3(x, y, 1);
            GameObject enemyClone = Instantiate(enemyPrefab, position, Quaternion.identity);
            SpawnHp(enemyClone);
        }
        yield return new WaitForSeconds(spawnTime);
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
