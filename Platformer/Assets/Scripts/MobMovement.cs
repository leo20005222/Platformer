using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    public float speed = 2f; // 앤티티의 이동 속도

    private Vector3 startPosition;
    private int direction = 1;
    private bool canMove = false; // ture = 움직임 가능

    void Start()
    {
        StartMoving();
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
    public void StartMoving()
    {
        canMove = true;
    }

    // 움직임 정지 (외부에서 호출 가능)
    public void StopMoving()
    {
        canMove = false;
    }

        void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 오브젝트의 태그가 "Mob"일 때
        if (collision.gameObject.CompareTag("Enemy"))
        {
            direction *= -1; // 방향 전환
        }
        // 충돌한 오브젝트의 태그가 "Wall"일 때 ##### 벽 통과시 태그 확인!
        if (collision.gameObject.CompareTag("Wall"))
        {
            direction *= -1; // 방향 전환
        }
    }
}
