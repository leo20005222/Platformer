using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 적 오브젝트의 RigidBody
    public Rigidbody2D rigidbody2D1;
    // 적 오브젝트의 이동속도
    public float walk_speed;

    // 적이 이동하는 경로의 제일 왼족 끝 바닥 오브젝트
    public GameObject left_end_point;
    // 적이 이동하는 경로의 제일 오른쪽 끝 바닥 오브젝트
    public GameObject right_end_point;

    // 적 오브젝트의 스프라이트 렌더러
    public SpriteRenderer spriteRenderer;

    // 적이 현재 왼쪽을 보고 있는지 여부
    bool is_left;
    // 적 이동방향(왼쪽 -1, 오른쪽 1)
    float dir;

    private void Start()
    {
        // 시작 시 왼쪽방향으로 설정
        is_left = true;
        dir = -1;
    }

    private void Update()
    {
        // 현재 이동 방향으로 적 오브젝트에 힘을 주어 밀기(이동)
        rigidbody2D1.AddForce(Vector2.right * dir, ForceMode2D.Impulse);
        // 적 이동속도 무한 가속 제한
        LimitSpeed();

        // 왼쪽으로 이동중이었고 현재 적 위치의 x값이 왼쪽 끝 오브젝트의 x값보다 작으면 이동경로의 왼쪽 끝에 도달했다는 의미
        if (transform.position.x <= left_end_point.transform.position.x && is_left)
        {
            // 적 오브젝트의 속도를 0으로 만들어 멈추고
            rigidbody2D1.velocity = Vector2.zero;

            // 방향을 오른쪽으로 설정
            is_left = false;
            dir = 1;

        }
        // 오른쪽으로 이동중이었고 현재 적 위치의 x값이 오른쪽 끝 오브젝트의 x값보다 크면 이동경로의 오른쪽 끝에 도달했다는 의미
        else if (transform.position.x >= right_end_point.transform.position.x && !is_left)
        {
            // 적 오브젝트의 속도를 0으로 만들어 멈추고
            rigidbody2D1.velocity = Vector2.zero;

            // 방향을 왼쪽으로 설정
            is_left = true;
            dir = -1;

        }
        if (dir == 1)
        {
            spriteRenderer.flipX = false;
        }
        // 왼쪽이면 뒤집어서 방향 전환
        else if (dir == -1)
        {
            spriteRenderer.flipX = true;
        }
    }

    // 적 이동속도 무한 가속 제한
    public void LimitSpeed()
    {
        // 좌우 움직이는 속도 x값이 최대 속도보다 크면
        if (rigidbody2D1.velocity.x > walk_speed)
        {
            // x속도를 최대 속도 값으로 고정
            // y속도는 기존 속도 값으로 그대로
            rigidbody2D1.velocity = (Vector2.right * walk_speed) + (Vector2.up * rigidbody2D1.velocity.y);
        }
        // 왼쪽으로 움직이는 속도값은 음수로 표현
        // 좌우 움직이는 속도 x값이 (최대 속도값 * -1) 보다 작으면
        else if (rigidbody2D1.velocity.x < walk_speed * -1)
        {
            // x속도를 최대 속도 값으로 고정, 방향은 왼쪽으로
            // y속도는 기존 속도 값으로 그대로
            rigidbody2D1.velocity = (Vector2.left * walk_speed) + (Vector2.up * rigidbody2D1.velocity.y);
        }
    }
}
