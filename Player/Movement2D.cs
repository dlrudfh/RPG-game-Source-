using UnityEngine;

public class Movement2D : MonoBehaviour
{
	private float moveSpeed;
	private Vector3 moveDirection;
	public int jumpCount; // 점프횟수(이단점프 활용)
	private bool doNotJump; // 점프를 할 수 없는 상태
	public bool doNotDash; // 대시를 할 수 없는 상태
	private Animator anime;

	public void Setup(float speed, Vector3 direction)
	{
		moveDirection = direction;
		moveSpeed = speed;
	}

	// 점프
    public void Jump(Animator a, AudioSource j)
    {
		anime = a;
		// 점프를 할 수 있는 상태라면
		if (doNotJump == false)
        {
			// 점프 변수 true(점프 애니메이션 재생)
			anime.SetBool("isJumping", true);
			// 점프카운트가 1 이하일 때(점프 가능 횟수가 남아있다면)
			if (jumpCount <= 1)
            {
				j.Play();
				jumpCount++;
				// 위로 10의 속도로 점프
				GetComponent<Rigidbody2D>().velocity = Vector2.up * 10;
			}
		}
	}

	// 하향 점프
	public void Down(Animator a, AudioSource j)
	{
		anime = a;
		// 플레이어의 y축 속도가 0일 경우(하향점프를 하려면 바닥에 닿아 있어야 하기 때문)
		if (GetComponent<Rigidbody2D>().velocity.y == 0)
		{
			// 하강 변수 true(하강 애니메이션 재생)
			anime.SetBool("isFalling", true);
			j.Play();
			PlayerPrefs.SetInt("FALL", 1);
		}
	}

	// 대시
	public void Dash(int dir, Animator a, AudioSource d)
    {
		// 대시를 할 수 있는 상태라면
		if(doNotDash == false)
        {
			d.Play();
			// 대시 중에는 대시, 점프 모두 불가능
			doNotDash = true;
			doNotJump = true;
			anime = a;
			// 대시 변수 true(대시 애니메이션 재생)
			anime.SetBool("isDashing", true);
			// 함수의 인자로 주어진 플레이어 방향 값을 기반으로 15의 속도로 전방으로 대시
			gameObject.GetComponent<Rigidbody2D>().velocity = ((dir==1 ? Vector2.right : Vector2.left)) * 15.0f;
			// 대시 중에는 아래로 떨어지지 않음
			gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
			// 0.2초 동안 대시 후 종료
			Invoke("DashEnd", 0.2f);
		}
    }

	// 대시 종료
	private void DashEnd()
    {
		// 대시 애니메이션 종료, 하강 애니메이션 실행(바닥에서 대시를 사용했을 경우
		// 하강 애니메이션이 중지되고 idle 애니메이션으로 자동 전환됨)
		anime.SetBool("isFalling", true);
		anime.SetBool("isDashing", false);
		// 대시가 종료되었으므로 점프 가능
		doNotJump = false;
		// 플레이어의 중력 값 복구
		gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		gameObject.GetComponent<Rigidbody2D>().gravityScale = 3;
		// 대시 레벨에 따른 쿨타임 부여
		Invoke("DoNotDash", 3.3f-(0.3f*PlayerPrefs.GetInt("DASHLV")));
	}

	private void DoNotDash()
    {
		// Invoke를 통해 쿨타임 만큼의 시간이 경과한 뒤 다시 대시가 가능해짐
		doNotDash = false;
    }

	private void Update()
	{
		// 새로운 위치 = 현재 위치 + (방향 * 속도)
		transform.position += moveDirection * moveSpeed * Time.deltaTime;
	}
}

