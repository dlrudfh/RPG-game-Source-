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
        PlayerPrefs.SetFloat("MUSIC", volume);
    }

    public void EffectVolume(float volume)
    {
        PlayerPrefs.SetFloat("EFFECT", volume);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void KeySetting(string key)
    {
        PlayerPrefs.SetString("KEY", key);
        InputWindow.SetActive(true);
    }

}
