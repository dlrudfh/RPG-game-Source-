using TMPro;
using UnityEngine;

public class TimeAttack : MonoBehaviour
{
    float time;
    public float timeAttack = 10; // ���ѽð�
    GameObject darkness; // �þ� ���� ������Ʈ
    public GameObject player;
    public Transform tilemap; //��ŸƮ���� Ÿ��(���������� �̵��� �����ϴ� ������Ʈ�� startline�̰�
                              // �ش� Ʈ�������� �ܼ��� ��ŸƮ������ ������ �����
    public Transform startline; // ��ŸƮ����(Ÿ�Ӿ����� Ȱ��ȭ���� �ʰ� �ʿ� �������� ���ϰ� �ϱ� ���� ���Ƶ�)

    void Start()
    {
        // �÷��̾��� �þ� ����
        darkness = GameObject.Find("emptyForDarkness").transform.GetChild(0).gameObject;
    }

    void Update()
    {
        //�ð� ����
        time += Time.deltaTime;
        // �Ϲ� ���ӿ��� ī��Ʈ�ٿ��� �ϵ��� 3���� �����ð��� �ΰ� Ÿ�Ӿ����� ������.
        if (time < 1.5f) GetComponent<TextMeshProUGUI>().text = "Ready...";
        else if (time < 3) GetComponent<TextMeshProUGUI>().text = "Start!!!";
        // Ÿ�Ӿ��� ���� �� ������ ��������
        else if (time < timeAttack + 3)
        {
            // ��ŸƮ���� Ÿ�� ����
            tilemap.GetComponent<TileChange>().ChangeTiles();
            // �̵��� ���� �ִ� ��ŸƮ���� ��Ȱ��ȭ
            GameObject.Find("walls").transform.Find("Timeattack").transform.Find("startline").gameObject.SetActive(false);
            // �÷��̾� �̵� ����
            player.GetComponent<Player>().enabled = true;
            // �þ� ���� ��� Ȱ��ȭ
            darkness.SetActive(true);
            // ȭ�� ��ܿ� ���� �ð� ǥ��
            GetComponent<TextMeshProUGUI>().text = (timeAttack + 3 - Mathf.Ceil(time)).ToString();
        }
        //Ÿ�Ӿ��� ����
        else 
        {
            // Ÿ�Ӿ��� UI�� �þ� ���� ������Ʈ ��Ȱ��ȭ
            gameObject.SetActive(false);
            darkness.SetActive(false);
            // ��ŸƮ���� ��Ȱ��ȭ
            startline.gameObject.SetActive(true);
            // Ÿ�Ӿ��� ���� ���� ����� Ÿ�� ����
            tilemap.GetComponent<TileChange>().RecoverTiles();
            //�÷��̾� ��ġ ����
            player.transform.localPosition = new Vector3(-17, -2.5f, 0);
        }
            
    }

    // �ش� ������Ʈ�� Ȱ��ȭ�Ǿ��� �� �ڵ����� ����
    void OnEnable()
    {
        // ��ũ��Ʈ ���࿡ �ʿ��� ������Ʈ Ž�� �� ��Ÿ �۾� ����
        player = GameObject.FindGameObjectWithTag("Player");
        tilemap = GameObject.Find("Grid").transform.Find("Tilemap");
        startline = GameObject.Find("walls").transform.Find("Timeattack").transform.Find("startline");
        time = 0;
        player.GetComponent<Player>().enabled = false;
    }
}
