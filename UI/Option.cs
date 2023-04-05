using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour, IDragHandler
{
    RectTransform rectTransform;
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject InputWindow;
    public float musicVol;
    public float effectVol;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject.Find("Main Camera").GetComponent<AudioSource>().volume = musicVol;
        GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().volume = effectVol;
        if (GameObject.FindGameObjectWithTag("PORTAL") != null)
            GameObject.FindGameObjectWithTag("PORTAL").GetComponent<AudioSource>().volume = effectVol;
        if (GameObject.FindGameObjectWithTag("CHEST") != null)
            GameObject.FindGameObjectWithTag("CHEST").GetComponent<AudioSource>().volume = effectVol;
    }

    public void MusicVolume(float volume)
    {
        musicVol = volume;
        GameObject.Find("Main Camera").GetComponent<AudioSource>().volume = musicVol;
    }

    public void EffectVolume(float volume)
    {
        effectVol = volume;
        GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().volume = effectVol;
        if(GameObject.FindGameObjectWithTag("PORTAL")!=null)
            GameObject.FindGameObjectWithTag("PORTAL").GetComponent<AudioSource>().volume = effectVol;
        if(GameObject.FindGameObjectWithTag("CHEST")!=null)
            GameObject.FindGameObjectWithTag("CHEST").GetComponent<AudioSource>().volume = effectVol;
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
