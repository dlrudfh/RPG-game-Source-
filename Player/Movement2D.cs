using UnityEngine;

public class Movement2D : MonoBehaviour
{
	private float moveSpeed;        // 이동 속도
	private Vector3 moveDirection;
	private int jumpCount;
	private bool doNotJump;
	private bool doNotDash;
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
			if (gameObject.GetComponent<Rigidbody2D>().velocity.y == 0)
			{
				j.Play();
				if (jumpCount > 1) jumpCount = 0;
				jumpCount++;
				gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * 10.0f;
			}
			else if (jumpCount <= 1)
			{
				j.Play();
				jumpCount++;
				gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * 10.0f;
			}
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
		Invoke("DoNotDash", 3.3f-(0.3f*PlayerPrefs.GetInt("DASH")));
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

