using UnityEngine;
using UnityEngine.EventSystems;

// �ɼ�UIâ ���� ��ũ��Ʈ
public class Option : MonoBehaviour, IDragHandler
{
    RectTransform rectTransform;
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject InputWindow;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    //UI�� �����ϴ� �����̴��� �̿��� ���� ���� ���� ����
    public void MusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MUSIC", volume);
    }

    //UI�� �����ϴ� �����̴��� �̿��� ȿ���� ������ ����
    public void EffectVolume(float volume)
    {
        PlayerPrefs.SetFloat("EFFECT", volume);
    }

    // Optionâ�� ���콺�� �巡�װ� �����ϴ�.
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    // Ű���� �Լ�
    public void KeySetting(string key)
    {
        // ������ ���ϴ� Ű ���� string���� �޾ƿ� �� KEY�� ����
        PlayerPrefs.SetString("KEY", key);
        // ����Ű �Է� �ȳ� â�� Ȱ��ȭ
        InputWindow.SetActive(true);
    }

}
