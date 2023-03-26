using TMPro;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    int level;
    int exp;
    int maxHp;
    int curHp;

    private void Update()
    {
        level = PlayerPrefs.GetInt("LV");
        exp = PlayerPrefs.GetInt("XP");
        maxHp = PlayerPrefs.GetInt("HP");
        curHp = PlayerPrefs.GetInt("CHP");
        gameObject.GetComponent<TextMeshProUGUI>().text = "\n<color=#f000ff>LV." + level + //ºÐÈ«
                                                          "\n<color=#000000>" + curHp + "/" + maxHp + //°ËÁ¤
                                                          "\n<color=#000000>" + (float)exp / level * 100 + "%";
    }
}
