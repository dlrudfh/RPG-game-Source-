using TMPro;
using UnityEngine;

// ���� �޽��� ��� ��ũ��Ʈ(ȭ�� �߾ӿ� ��µǴ� ������ �޽��� ��)
public class Notice : MonoBehaviour
{
    private void Update()
    {
        // Notice���ڿ��� ���� �Էµ� ���
        if (PlayerPrefs.GetString("Notice") != "")
        {
            //�ش� ���ڿ��� ������ ��� �� Notice�� �ʱ�ȭ
            GetComponent<TextMeshProUGUI>().text = (PlayerPrefs.GetString("Notice"));
            PlayerPrefs.SetString("Notice", "");
            // ������ 2�� �� ���ŵ�
            Invoke("clean", 2f);
        }
    }

    private void clean()
    {
        GetComponent<TextMeshProUGUI>().text = "";
    }
}
