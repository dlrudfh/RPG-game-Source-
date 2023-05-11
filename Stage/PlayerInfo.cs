using TMPro;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    int level;
    float exp;
    float maxHp;
    float curHp;

    private void Update()
    {
        level = PlayerPrefs.GetInt("LV");
        exp = PlayerPrefs.GetFloat("XP");
        maxHp = PlayerPrefs.GetFloat("HP");
        curHp = PlayerPrefs.GetFloat("CHP");
        gameObject.GetComponent<TextMeshProUGUI>().text = "\n<color=#f000ff>LV." + level + //��ȫ
                                                          "\n<color=#000000>" + curHp + "/" + maxHp + //����
                                                          "\n<color=#000000>" + exp / level * 100 + "%";
    }
}
