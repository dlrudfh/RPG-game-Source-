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
