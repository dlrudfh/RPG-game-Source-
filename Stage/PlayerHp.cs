using UnityEngine.UI;
using UnityEngine;

public class PlayerHp : MonoBehaviour
{
    private void Update()
    {
        // �÷��� ȭ�� ���� ����� ü�� �� ����
        GetComponent<Slider>().value = PlayerPrefs.GetFloat("CHP")
                                     / PlayerPrefs.GetFloat("HP");
    }
}
