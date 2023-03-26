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
        DMG.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("DMG").ToString();
        HP.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("CHP").ToString() + "/" + PlayerPrefs.GetInt("HP").ToString();
        XP.GetComponent<TextMeshProUGUI>().text = (float)PlayerPrefs.GetInt("XP") / PlayerPrefs.GetInt("LV") * 100 + "%";
        GOLD.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("GOLD") + "G";
        PTS.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("PTS") + "PTS";
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 이전 이동과 비교해서 얼마나 이동했는지를 보여줌
        // 캔버스의 스케일과 맞춰야 하기 때문에
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

}