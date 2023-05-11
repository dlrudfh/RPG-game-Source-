using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Status : MonoBehaviour, IDragHandler
{

    RectTransform rectTransform;
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject LV;
    [SerializeField] GameObject DMG;
    [SerializeField] GameObject HP;
    [SerializeField] GameObject XP;
    [SerializeField] GameObject GOLD;
    [SerializeField] GameObject PTS;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        LV.GetComponent<TextMeshProUGUI>().text = "LV." + PlayerPrefs.GetInt("LV");
        DMG.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetFloat("DMG").ToString();
        HP.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetFloat("CHP") + "/" + PlayerPrefs.GetFloat("HP");
        XP.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetFloat("XP") / PlayerPrefs.GetInt("LV") * 100 + "%";
        GOLD.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("GOLD") + "G";
        PTS.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("PTS") + "PTS";
    }

    public void OnDrag(PointerEventData eventData)
    {
        // ���� �̵��� ���ؼ� �󸶳� �̵��ߴ����� ������
        // ĵ������ �����ϰ� ����� �ϱ� ������
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

}