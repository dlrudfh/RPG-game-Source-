using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Quest : MonoBehaviour, IDragHandler
{
    private int questCount = 0;
    [SerializeField] private int c = 0;
    [SerializeField] private bool complete;
    [SerializeField] GameObject questTab;
    RectTransform rectTransform;
    [SerializeField] Canvas canvas;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("CurrentQuest") != 0)
        {
            int i = PlayerPrefs.GetInt("CurrentQuest");
            GameObject questTabClone = Instantiate(questTab, new Vector3(0, 0, 1), Quaternion.identity);
            questTabClone.name = i.ToString();
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
        des.Find("script").GetComponent<TextMeshProUGUI>().text = QuestSummary.description[n];
        des.Find("reward").GetComponent<TextMeshProUGUI>().text = "Reward : " + QuestSummary.reward[n] + "G";
        if ((QuestSummary.progress[n] >= QuestSummary.goal[n]))
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
            QuestSummary.Complete(c);
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
