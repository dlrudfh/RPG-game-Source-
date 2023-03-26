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
        // ���� �̵��� ���ؼ� �󸶳� �̵��ߴ����� ������
        // ĵ������ �����ϰ� ����� �ϱ� ������
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void KeySetting(string key)
    {
        PlayerPrefs.SetString("KEY", key);
        InputWindow.SetActive(true);
    }

}
