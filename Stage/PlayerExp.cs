using UnityEngine.UI;
using UnityEngine;

public class PlayerExp : MonoBehaviour
{
    private void Update()
    {
        // �÷��� ȭ�� ���� ����� ����ġ �� ����
        GetComponent<Slider>().value = PlayerPrefs.GetFloat("XP")
                                     / PlayerPrefs.GetInt("LV");
    }
}