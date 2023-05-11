using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class KeySetting : MonoBehaviour
{

    private void OnGUI()
    {
        Event e = Event.current;
        if (e != null && e.type == EventType.KeyDown)
        {
            string key = PlayerPrefs.GetString("KEY");
            //if(Regex.IsMatch(e.keyCode.ToString(), @"[A-Z]$")){
                PlayerPrefs.SetString(key, e.keyCode.ToString());
            //}
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Key();
            GameObject.FindGameObjectWithTag(key).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString(key);
            transform.parent.parent.GetComponentInParent<UIinfo>().stKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("STATUS"), true);
            transform.parent.parent.GetComponentInParent<UIinfo>().skKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("SKILL"), true);
            transform.parent.parent.GetComponentInParent<UIinfo>().opKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("OPTION"), true);
            transform.parent.parent.GetComponentInParent<UIinfo>().quKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("QUEST"), true);
            gameObject.SetActive(false);
        }
    }
}
