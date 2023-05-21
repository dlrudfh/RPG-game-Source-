using System.Collections;
using UnityEngine;

// ���� ������Ʈ�� �����ϴ� ��ũ��Ʈ
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
    private float spawnTime; // �� ���� �ֱ�
    private int spawnCount; // �ʿ� ������ �� ��
    Transform walls; // �� ������Ʈ�� ��� ���� �θ� ������Ʈ
    [SerializeField] bool spawn; // �ش� �ʿ��� �� ��ȯ�� �� ������ ����(inspector���� ���� ����)
    [SerializeField] int spawnLimit = 20; // �� �ִ� ���� ����

    private void Awake()
    {
        // �� ��ȯ�� ������ ���� ���
        if (spawn)
        {
            // �� ������Ʈ�� Ž��
            walls = GameObject.Find("walls").transform;
            // ���� �ڷ�ƾ ����
            StartCoroutine("SpawnStart");
        }
        PlayerPrefs.SetInt("spawnCount", 0);
        // �÷��̾ �־��� ��ǥ�� ����
        GameObject p = Instantiate(player, new Vector3(PlayerPrefs.GetFloat("x"), PlayerPrefs.GetFloat("y"), 0), Quaternion.identity);
        // �÷��̾��� �̵��ݰ��� �����ϱ� ���� stagedata ����
        p.GetComponent<Player>().stage = stage;
    }

    // �� ��ȯ �Լ�
    private IEnumerator SpawnStart()
    {
        yield return new WaitForSeconds(spawnTime);
        spawnCount = PlayerPrefs.GetInt("spawnCount");
        // �ʿ� �����ϴ� ���� �� �ϳ��� ����
        Transform floor = walls.GetChild(Random.Range(0, walls.childCount));
        while (true)
        {
            // ���� �ݺ��ϸ鼭 �� ������ ������ ���� ���õ� ������ �ݺ�
            // (Ư�� �������� �� ������ �Ұ����ϵ��� �����ϱ� ���� ����)
            if(floor.TryGetComponent(out Wall w))
                if (w.canSpawn) break;
            floor = walls.GetChild(Random.Range(0, walls.childCount));
        }
        // �� ���� ������ �ʰ����� �ʾ��� ��
        if (spawnCount <= spawnLimit)
        {
            spawnCount++;
            PlayerPrefs.SetInt("spawnCount", spawnCount);
            // ���õ� ������ ������ ��ġ�� �� ����
            float x = Random.Range(floor.position.x - floor.localScale.x/2 + 0.5f, floor.position.x + floor.localScale.x/2 - 2.5f);
            float y = floor.position.y + 1; 
            Vector3 position = new Vector3(x, y, 1);
            GameObject enemyClone = Instantiate(enemyPrefab, position, Quaternion.identity);
            // ���� hp�ٵ� �Բ� ����
            SpawnHp(enemyClone);
        }
        //�ڷ�ƾ �ݺ�
        StopCoroutine("SpawnStart");
        StartCoroutine("SpawnStart");
    }

    //�� ü�� �� ���� �Լ�
    private void SpawnHp(GameObject enemy)
    {
        //ü�� �� ����
        GameObject sliderClone = Instantiate(enemyHp);
        // ü�� �ٴ� UI�̱� ������ ĵ����(System)�� �ڽ� ������Ʈ�� ��ӵǾ�� ��
        sliderClone.transform.SetParent(GameObject.Find("System").transform);
        sliderClone.transform.localScale = Vector3.one;
        // ü�� �ٰ� ���� �����ϰ�, �� ü�� ������ �°�
        // �����̴� ���� �����ϱ� ���� ������Ʈ ����
        sliderClone.GetComponent<SpawnedUISet>().Setup(enemy.transform);
        sliderClone.GetComponent<EnemyHpViewer>().Setup(enemy.GetComponent<Enemy>());
    }
}
