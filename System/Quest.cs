using TMPro;
using UnityEngine;

public class Quest : MonoBehaviour
{
    private int questCount = 0;
    [SerializeField] private int c = 0;
    [SerializeField] private bool complete;
    [SerializeField] GameObject questTab;
    void Update()
    {
        if (PlayerPrefs.GetInt("CurrentQuest") != 0)
        {
            int i = PlayerPrefs.GetInt("CurrentQuest");
            GameObject questTabClone = Instantiate(questTab, new Vector3(0, 0, 1), Quaternion.identity);
            questTabClone.name = PlayerPrefs.GetInt("CurrentQuest").ToString();
            PlayerPrefs.SetInt("CurrentQuest", 0);
            questTabClone.transform.SetParent(gameObject.transform);
            questTabClone.transform.localScale = new Vector3(1, 1, 1);
            questTabClone.transform.localPosition = new Vector3(0, 270 - 60 * questCount++, 1);
            questTabClone.GetComponent<QuestTab>().n = i;
            questTabClone.GetComponentInChildren<TextMeshProUGUI>().text = QuestSummary.title[i];
        }
    }
    public void Description(int n)
    {
        Transform des = transform.GetChild(1);
        des.gameObject.SetActive(true);
        c = n;
        des.GetComponentInChildren<TextMeshProUGUI>().text = QuestSummary.description[n];
        if((QuestSummary.progress[n] >= QuestSummary.goal[n]))
        {
            complete = true;
            des.Find("Complete").GetComponentInChildren<TextMeshProUGUI>().text = "Complete";
        }
        else
        {
            des.Find("Complete").GetComponentInChildren<TextMeshProUGUI>().text = QuestSummary.progress[n]+"/"+QuestSummary.goal[n];
        } 
    }

    public void Complete()
    {
        if (complete)
        {
            PlayerPrefs.SetInt("GOLD", PlayerPrefs.GetInt("GOLD") + QuestSummary.reward[c]);
            Destroy(GameObject.Find(c.ToString()));
            DesOff();
        }
        
    }

    public void DesOff()
    {
        c = 0;
        complete = false;
        GameObject.Find("Description").SetActive(false);
    }
}