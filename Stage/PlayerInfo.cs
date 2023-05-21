using TMPro;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    private void Update()
    {
        // ȭ�� ���� ��ܿ� �÷��̾� ���� ��� UI���� �ؽ�Ʈ �κ��� ����� 
        gameObject.GetComponent<TextMeshProUGUI>().text = "\n<color=#f000ff>LV." + PlayerPrefs.GetInt("LV") + //��ȫ
                                                          "\n<color=#000000>" + PlayerPrefs.GetFloat("CHP") + "/" + PlayerPrefs.GetFloat("HP") + //����
                                                          "\n<color=#000000>" + PlayerPrefs.GetFloat("XP") / PlayerPrefs.GetInt("LV") * 100 + "%";
    }
}
