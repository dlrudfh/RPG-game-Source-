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
        PlayerPrefs.SetInt("CHP", 3);
        PlayerPrefs.SetInt("LV", 1);
        PlayerPrefs.SetInt("PTS", 0);
        PlayerPrefs.SetInt("DMG", 1);
        PlayerPrefs.SetInt("XP", 0);
        PlayerPrefs.SetInt("HP", 3);
        PlayerPrefs.SetInt("GOLD", 0);
        PlayerPrefs.SetInt("CHARGESHOT", 0);
        PlayerPrefs.SetInt("DASH", 0);
        PlayerPrefs.SetString("LEFT", "LeftArrow");
        PlayerPrefs.SetString("RIGHT", "RightArrow");
        PlayerPrefs.SetString("JUMP", "Z");
        PlayerPrefs.SetString("ATTACK", "X");
        PlayerPrefs.SetString("DASH", "C");
        PlayerPrefs.SetString("ACTION", "Space");
        PlayerPrefs.SetInt("CurrentQuest", 0);
        stKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), "S", true);
        skKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), "K", true);
        opKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Escape", true);
        quKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Q", true);
    }

    // Update is called once per frame
    void Update()
    {
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
            if (quest.activeSelf && quest.transform.Find("Description").gameObject.activeSelf)
            {
                quest.GetComponent<Quest>().DesOff();
                quest.SetActive(false);
            }
            else quest.SetActive(!quest.activeSelf);
        }
    }
}
