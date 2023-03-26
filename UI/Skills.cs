using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Skills : MonoBehaviour, IDragHandler
{

    RectTransform rectTransform;
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject ChargeShot;
    [SerializeField] GameObject Dash;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if(PlayerPrefs.GetInt("LV") < 3) 
            ChargeShot.GetComponent<TextMeshProUGUI>().text = "Required LV.3";
        else 
            ChargeShot.GetComponent<TextMeshProUGUI>().text = "ChargeShot LV." + PlayerPrefs.GetInt("CHARGESHOT");

        if (PlayerPrefs.GetInt("LV") < 5)
            Dash.GetComponent<TextMeshProUGUI>().text = "Required LV.5";
        else
            Dash.GetComponent<TextMeshProUGUI>().text = "Dash LV." + PlayerPrefs.GetInt("DASH");

    }
    public void OnDrag(PointerEventData eventData)
    {
        // 이전 이동과 비교해서 얼마나 이동했는지를 보여줌
        // 캔버스의 스케일과 맞춰야 하기 때문에
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}