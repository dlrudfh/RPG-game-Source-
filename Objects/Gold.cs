using System.Collections;
using UnityEngine;

public class Gold : MonoBehaviour
{
    private bool alreadyPick;
    private AudioSource pick;

    private void Awake()
    {
        pick = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && alreadyPick == false)
        {
            alreadyPick = true;
            if(CompareTag("coin")) PlayerPrefs.SetInt("GOLD", PlayerPrefs.GetInt("GOLD")+1);
            else PlayerPrefs.SetInt("GOLD", PlayerPrefs.GetInt("GOLD") + 5);
            Pick();
        }
        else if (collision.CompareTag("wall"))
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Rigidbody2D>().velocity = Vector2.up * 0;
        }
    }

	public void Drop()
	{
		GetComponent<Rigidbody2D>().velocity = Vector2.up * 7.0f;
        GetComponent<Rigidbody2D>().gravityScale = 3;
	}

    public void Pick()
    {
        pick.Play();
        GetComponent<Rigidbody2D>().velocity = Vector2.up * 7.0f;
        GetComponent<Rigidbody2D>().gravityScale = 3;
        StartCoroutine("FadeOut");
        Destroy(gameObject, 0.5f);
    }

    private IEnumerator FadeOut()
    {
        float a = 1;
        while (a != 0 && CompareTag("coin")) 
        {
            a = GetComponent<SpriteRenderer>().color.a;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 0, a - 0.02f);
            yield return new WaitForSeconds(0.01f);
        }
        while (a != 0 && CompareTag("money"))
        {
            a = GetComponent<SpriteRenderer>().color.a;
            GetComponent<SpriteRenderer>().color = new Color(0, 1, 1, a - 0.02f);
            yield return new WaitForSeconds(0.01f);
        }
    }

}
