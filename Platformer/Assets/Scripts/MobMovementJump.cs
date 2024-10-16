using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EntityMovementJump : MonoBehaviour
{
    public float speed = 2f; // 앤티티의 이동 속도
    public float jumpForce = 8f; // 점프할 때의 힘
    public float jumpInterval = 2f; // 점프 간격 (초 단위)

    private Vector3 startPosition;
    private int direction = 1;
    private bool canMove = false; // true = 움직임 가능
    private bool isGrounded = true; // 바닥에 있는지 여부
    private Rigidbody2D rb;

    void Start()
    {
        // 초기 위치 저장 (방향 전환, 거리 계산)
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        StartMovingJump();

        // 일정 시간마다 점프하는 함수 호출
        InvokeRepeating("Jump", jumpInterval, jumpInterval);
    }

    void Update()
    {
        // canMove가 true일 때만 앤티티 이동
        if (canMove)
        {
            transform.Translate(Vector2.right * speed * direction * Time.deltaTime);
        }
    }

    // 움직임 시작 (외부에서 호출 가능)
    public void StartMovingJump()
    {
        canMove = true;
    }

    // 움직임 정지 (외부에서 호출 가능)
    public void StopMovingJump()
    {
        canMove = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 오브젝트의 태그가 "Mob"일 때
        if (collision.gameObject.CompareTag("Mob"))
        {
            direction *= -1; // 방향 전환
        }
        // 충돌한 오브젝트의 태그가 "Wall"일 때
        if (collision.gameObject.CompareTag("Wall"))
        {
            direction *= -1; // 방향 전환
        }

        // 충돌한 오브젝트가 "Ground" 태그를 가질 때 바닥에 있다는 표시
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // 앤티티가 바닥에서 떨어졌을 때
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    // 점프 기능 추가
    void Jump()
    {
        // 바닥에 있을 때만 점프
        if (isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false; // 점프를 한 이후에는 공중에 있게 됨
        }
    }
}