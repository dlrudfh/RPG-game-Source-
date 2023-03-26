using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class KeySetting : MonoBehaviour
{
    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnGUI()
    {
        Event e = Event.current;
        if (e != null && e.type == EventType.KeyDown)
        {
            string key = PlayerPrefs.GetString("KEY");
            //if(Regex.IsMatch(e.keyCode.ToString(), @"[A-Z]$")){
                PlayerPrefs.SetString(key, e.keyCode.ToString());
            //}
            player.GetComponent<Player>().Key();
            GameObject.FindGameObjectWithTag(key).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString(key);
            transform.parent.parent.GetComponentInParent<UIinfo>().stKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("STATUS"), true);
            transform.parent.parent.GetComponentInParent<UIinfo>().skKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("SKILL"), true);
            transform.parent.parent.GetComponentInParent<UIinfo>().opKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("OPTION"), true);
            transform.parent.parent.GetComponentInParent<UIinfo>().quKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("QUEST"), true);
            gameObject.SetActive(false);
        }
    }
}
