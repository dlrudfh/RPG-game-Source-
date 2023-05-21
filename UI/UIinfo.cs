using UnityEngine;

public class UIinfo : MonoBehaviour
{
    [SerializeField]
    private GameObject status;
    [SerializeField]
    private GameObject skill;
    [SerializeField]
    private GameObject option;
    [SerializeField]
    private GameObject quest;
    public KeyCode stKey;
    public KeyCode skKey;
    public KeyCode opKey;
    public KeyCode quKey;

    private void Awake()
    {   
        // 플레이어 정보 초기화(테스트용)
        PlayerPrefs.SetFloat("CHP", 3);
        PlayerPrefs.SetInt("LV", 1);
        PlayerPrefs.SetInt("PTS", 100);
        PlayerPrefs.SetFloat("DMG", 100);
        PlayerPrefs.SetInt("XP", 0);
        PlayerPrefs.SetFloat("HP", 3);
        PlayerPrefs.SetInt("GOLD", 0);
        PlayerPrefs.SetInt("CHARGESHOTLV", 0);
        PlayerPrefs.SetInt("DASHLV", 0);
        PlayerPrefs.SetInt("HEALLV", 0);
        PlayerPrefs.SetString("LEFT", "LeftArrow");
        PlayerPrefs.SetString("RIGHT", "RightArrow");
        PlayerPrefs.SetString("JUMP", "Z");
        PlayerPrefs.SetString("DOWN", "DownArrow");
        PlayerPrefs.SetString("ATTACK", "X");
        PlayerPrefs.SetString("DASH", "C");
        PlayerPrefs.SetString("HEAL", "V");
        PlayerPrefs.SetString("ACTION", "Space");
        PlayerPrefs.SetInt("CurrentQuest", 0);
        PlayerPrefs.SetString("STATUS", "S");
        PlayerPrefs.SetString("SKILL", "K");
        PlayerPrefs.SetString("OPTION", "Escape");
        PlayerPrefs.SetString("QUEST", "Q");
        PlayerPrefs.SetFloat("MUSIC", 0.1f);
        PlayerPrefs.SetFloat("EFFECT", 0.1f);
        
        //빌드 직전에 한번 실행하고 종료 후 윗부분 주석처리
        //UI창 키세팅
        stKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("STATUS"), true);
        skKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("SKILL"), true);
        opKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("OPTION"), true);
        quKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("QUEST"), true);
    }

    void Update()
    {
        //UI창 온오프
        if (Input.GetKeyDown(stKey))
        {
            status.SetActive(!status.activeSelf);
        }
        else if (Input.GetKeyDown(skKey))
        {
            skill.SetActive(!skill.activeSelf);
        }
        else if (Input.GetKeyDown(opKey))
        {
            option.SetActive(!option.activeSelf);
        }
        else if (Input.GetKeyDown(quKey))
        {
            //퀘스트 설명 창 열려있으면 퀘스트 창과 함께 off하기
            if (quest.activeSelf && quest.transform.Find("Description").gameObject.activeSelf)
            {
                quest.GetComponent<Quest>().DesOff();
                quest.SetActive(false);
            }
            //아닐 경우 퀘스트 창만 off
            else quest.SetActive(!quest.activeSelf);
        }
    }

    // 기본 UI 온(인트로 페이지나 게임 오버 페이지에서 UI 안보이게 하기 위함)
    public void Activate()
    {
        transform.Find("PlayerExp").gameObject.SetActive(true);
        transform.Find("PlayerHp").gameObject.SetActive(true);
        transform.Find("PlayerInfo").gameObject.SetActive(true);
        transform.Find("Notice").gameObject.SetActive(true);
    }

    // 기본 UI 오프
    public void DeActivate()
    {
        for (int x = 0; x < transform.childCount; x++)
            transform.GetChild(x).gameObject.SetActive(false);
    }
}
