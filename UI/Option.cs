using UnityEngine;
using UnityEngine.EventSystems;

public class Option : MonoBehaviour, IDragHandler
{
    RectTransform rectTransform;
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject InputWindow;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void MusicVolume(float volume)
    {
        GameObject.Find("Main Camera").GetComponent<AudioSource>().volume = volume;
    }

    public void EffectVolume(float volume)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().volume = volume;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 이전 이동과 비교해서 얼마나 이동했는지를 보여줌
        // 캔버스의 스케일과 맞춰야 하기 때문에
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void KeySetting(string key)
    {
        PlayerPrefs.SetString("KEY", key);
        InputWindow.SetActive(true);
    }

}
