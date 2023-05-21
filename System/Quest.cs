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

    // ����Ʈ â UI�� ���콺�� �巡�װ� �����ϵ��� ��
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    void Update()
    {
        // ���ο� ����Ʈ�� ������ ���
        // CurrentQuest�� ��ҿ��� ���� 0������, ���ο� ����Ʈ�� �߻��ϸ� �ش� ����Ʈ�� ��ȣ ���� ������ ��
        if (PlayerPrefs.GetInt("CurrentQuest") != 0)
        {
            //����Ʈ ��ȣ�� i�� ����
            int i = PlayerPrefs.GetInt("CurrentQuest");
            // ����Ʈ UI�� �� ����
            GameObject questTabClone = Instantiate(questTab, new Vector3(0, 0, 1), Quaternion.identity);
            questTabClone.name = i.ToString();
            // ����Ʈ�� Ȱ��ȭ�Ǿ����Ƿ� �� �ʱ�ȭ
            PlayerPrefs.SetInt("CurrentQuest", 0);
            // ������ ����Ʈ ���� ����Ʈ UI�� �ڽ����� ����
            questTabClone.transform.SetParent(gameObject.transform);
            // ��ӵǾ����Ƿ� ������, ��ġ ����
            questTabClone.transform.localScale = new Vector3(1, 1, 1);
            // ����Ʈ ���� ��� ���� �������� ����Ʈ�� �ϳ��� ���̴� �����̱� ������
            // ����Ʈ ���� ���� ���� ��ġ�� ��ġ��
            questTabClone.transform.localPosition = new Vector3(0, 270 - 60 * questCount++, 1);
            // ����Ʈ �ǿ� ����Ʈ ��ȣ�� �ο���
            questTabClone.GetComponent<QuestTab>().n = i;
            // ����Ʈ �ǿ� �ش� ����Ʈ�� Ÿ��Ʋ�� �Է�
            questTabClone.GetComponentInChildren<TextMeshProUGUI>().text = QuestSummary.title[i];
        }
    }
    
    //����Ʈ ���� â
    // ����Ʈ ���� Ŭ�� �� ����Ʈ UI������ ���� â�� �����ϰ�, �ڼ��� ����� �Բ� ���൵�� ���� ���� Ȯ���� �� �ִ�.
    public void Description(int n)
    {
        // ����Ʈ ���� â ������Ʈ
        Transform des = transform.GetChild(1);
        //���� ������Ʈ Ȱ��ȭ
        des.gameObject.SetActive(true);
        c = n;
        // ����� ������ Ŭ���� ����Ʈ ��ȣ�� �°� �����
        des.Find("script").GetComponent<TextMeshProUGUI>().text = QuestSummary.description[n];
        des.Find("reward").GetComponent<TextMeshProUGUI>().text = "Reward : " + QuestSummary.reward[n] + "G";
        // ����Ʈ�� ���൵�� ���� ��°��� ��ȭ�� ��
        if ((QuestSummary.progress[n] >= QuestSummary.goal[n]))
        {
            // ����Ʈ�� �Ϸ�Ǿ��� ��� complete = true
            complete = true;
            // �Ϸ� ��ư�� Complete�� ��µ�
            des.Find("Complete").GetComponentInChildren<TextMeshProUGUI>().text = "Complete";
        }
        else
        {
            //����Ʈ�� ���� �Ϸ���� �ʾ��� ��� ���൵�� ��µ�
            des.Find("Complete").GetComponentInChildren<TextMeshProUGUI>().text = QuestSummary.progress[n]+"/"+QuestSummary.goal[n];
        } 
    }

    //����Ʈ �Ϸ�
    public void Complete()
    {
        if (complete)
        {
            // �Ϸ� ���� ����
            QuestSummary.Complete(c);
            //����Ʈ �� ���� �� ���� â�� ����
            Destroy(GameObject.Find(c.ToString()));
            DesOff();
        }
        
    }

    // ���� â �ݱ�
    public void DesOff()
    {
        c = 0;
        // �ٸ� ����Ʈ�� ������ �� ����Ʈ�� �Ϸ�ǵ��� ���� �ʱ� ���� complete = false
        complete = false;
        // ���� â ��Ȱ��ȭ
        GameObject.Find("Description").SetActive(false);
    }
}
