using UnityEngine;

public class Movement2D : MonoBehaviour
{
	private float moveSpeed;
	private Vector3 moveDirection;
	public int jumpCount;
	private bool doNotJump;
	public bool doNotDash;
	private Animator anime;

	public void Setup(float speed, Vector3 direction)
	{
		moveDirection = direction;
		moveSpeed = speed;
	}

    public void Jump(Animator a, AudioSource j)
    {
		anime = a;
		if (doNotJump == false)
        {
			anime.SetBool("isJumping", true);
			if (jumpCount <= 1)
            {
				j.Play();
				jumpCount++;
				GetComponent<Rigidbody2D>().velocity = Vector2.up * 10;
			}
		}
	}

	public void Down(Animator a, AudioSource j)
	{
		anime = a;
		if (GetComponent<Rigidbody2D>().velocity.y == 0)
		{
			anime.SetBool("isFalling", true);
			j.Play();
			PlayerPrefs.SetInt("FALL", 1);
		}
	}

	public void Dash(int dir, Animator a, AudioSource d)
    {
		if(doNotDash == false)
        {
			d.Play();
			doNotDash = true;
			doNotJump = true;
			anime = a;
			anime.SetBool("isDashing", true);
			gameObject.GetComponent<Rigidbody2D>().velocity = ((dir==1 ? Vector2.right : Vector2.left)) * 15.0f;
			gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
			Invoke("DashEnd", 0.2f);
		}
    }

	private void DashEnd()
    {
		anime.SetBool("isFalling", true);
		anime.SetBool("isDashing", false);
		doNotJump = false;
		gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		gameObject.GetComponent<Rigidbody2D>().gravityScale = 3;
		Invoke("DoNotDash", 3.3f-(0.3f*PlayerPrefs.GetInt("DASHLV")));
	}

	private void DoNotDash()
    {
		doNotDash = false;
    }

	private void Update()
	{
		// 새로운 위치 = 현재 위치 + (방향 * 속도)
		transform.position += moveDirection * moveSpeed * Time.deltaTime;
	}
}

