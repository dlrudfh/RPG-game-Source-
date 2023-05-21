using System.Collections;
using UnityEngine;

// ��ȭ ������Ʈ
public class Gold : MonoBehaviour
{
    private bool alreadyPick; // ȹ�� �̺�Ʈ�� �� ���� �۵��ǵ��� �����ϱ� ���� ����
    private AudioSource pick; // ȹ�� ȿ����

    private void Awake()
    {
        pick = GetComponent<AudioSource>();
    }

    private void Update()
    {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("EFFECT");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾ �ش� ������Ʈ�� ���ʷ� �浹���� ��
        if (collision.CompareTag("Player") && alreadyPick == false)
        {
            alreadyPick = true; // �̺�Ʈ ��ߵ� ������ ���� �� ����
            if(CompareTag("coin")) PlayerPrefs.SetInt("GOLD", PlayerPrefs.GetInt("GOLD")+1);
            else PlayerPrefs.SetInt("GOLD", PlayerPrefs.GetInt("GOLD") + 5);
            Pick();
        }
        else if (collision.CompareTag("wall")) // �ٴڿ� ����� ��� ����
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Rigidbody2D>().velocity = Vector2.up * 0;
        }
    }

	public void Drop()
	{
        // ���� ��� �� ������Ʈ�� ��� ���� Ƣ����� �� �ٴڿ� �������� �ִϸ��̼�
		GetComponent<Rigidbody2D>().velocity = Vector2.up * 7.0f;
        GetComponent<Rigidbody2D>().gravityScale = 3;
	}

    public void Pick()
    {
        // ȹ�� ȿ���� ���
        pick.Play();
        GetComponent<Rigidbody2D>().velocity = Vector2.up * 7.0f;
        GetComponent<Rigidbody2D>().gravityScale = 3;
        // ȹ�� ���� ���� ������� �������� ���ŵǴ� �ִϸ��̼�
        StartCoroutine("FadeOut");
        Destroy(gameObject, 0.5f);
    }

    // ������Ʈ ���� ��������
    private IEnumerator FadeOut()
    {
        float a = 1;
        while (a != 0 && CompareTag("coin")) 
        {
            a = GetComponent<SpriteRenderer>().color.a;
            // 50�����ӿ� ���� ���� ��������
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 0, a - 0.02f);
            yield return new WaitForSeconds(0.01f);
        }
        while (a != 0 && CompareTag("money"))
        {
            a = GetComponent<SpriteRenderer>().color.a;
            // 50�����ӿ� ���� ���� ��������
            GetComponent<SpriteRenderer>().color = new Color(0, 1, 1, a - 0.02f);
            yield return new WaitForSeconds(0.01f);
        }
    }

}
