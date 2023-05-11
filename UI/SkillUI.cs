using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SkillUI : MonoBehaviour, IDragHandler
{

    RectTransform rectTransform;
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject ChargeShot;
    [SerializeField] GameObject Dash;
    [SerializeField] GameObject Heal;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("LV") < 3)
            ChargeShot.GetComponent<TextMeshProUGUI>().text = "Required LV.3";
        else if (PlayerPrefs.GetInt("CHARGESHOTLV") == 10)
            ChargeShot.GetComponent<TextMeshProUGUI>().text = "Mastered";
        else
            ChargeShot.GetComponent<TextMeshProUGUI>().text = "ChargeShot LV." + PlayerPrefs.GetInt("CHARGESHOTLV");

        if (PlayerPrefs.GetInt("LV") < 5)
            Dash.GetComponent<TextMeshProUGUI>().text = "Required LV.5";
        else if (PlayerPrefs.GetInt("DASHLV") == 10)
            Dash.GetComponent<TextMeshProUGUI>().text = "Mastered";
        else
            Dash.GetComponent<TextMeshProUGUI>().text = "Dash LV." + PlayerPrefs.GetInt("DASHLV");

        if (PlayerPrefs.GetInt("LV") < 10)
            Heal.GetComponent<TextMeshProUGUI>().text = "Required LV.10";
        else if (PlayerPrefs.GetInt("HEALLV") == 10)
            Heal.GetComponent<TextMeshProUGUI>().text = "Mastered";
        else
            Heal.GetComponent<TextMeshProUGUI>().text = "Heal LV." + PlayerPrefs.GetInt("HEALLV");

    }
    public void OnDrag(PointerEventData eventData)
    {
        // ���� �̵��� ���ؼ� �󸶳� �̵��ߴ����� ������
        // ĵ������ �����ϰ� ����� �ϱ� ������
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}