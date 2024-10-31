using UnityEngine;

public class Player : MonoBehaviour
{
    public static GameManager Instance;
    // 인게임 메인 카메라
    public Camera cam;
    // 플레이어 Position y값과 카메라의 Position y값 차이
    public float cam_diff;

    // 맵 왼쪽 끝 벽
    public GameObject left_end_wall;
    // 맵 오른쪽 끝 벽
    public GameObject right_end_wall;

    // 플레이어의 Rigidbocy
    public Rigidbody2D rigidbody2D1;

    // 플레이어의 걷는 속도
    public float walk_speed;
    // 플레이어의 점프력
    public float jump_power;

    // 현재 무적상태 여부
    bool is_Invincibility;
    // 무적 시간 카운트 변수
    float invincibility_count;

    // 플레이어의 스프라이트 렌더러
    public SpriteRenderer spriteRenderer;
    // 플레이어의 애니메이터
    public Animator animator;
    // 플레이어의 현재 이동방향
    public float now_dir;

    // 점프 제한 횟수
    public int limit_jump_num;
    // 현재 점프중인지 여부
    public bool is_jump;

    // 최대 목숨 수
    public int max_life;
    // 현재 목숨 수
    public int now_life;

    // 리셋 시 플레이어 초기화 함수
    public void Init()
    {
        // 현재 목숨 수를 최대치로 설정
        now_life = max_life;
        // 처음 시작 위치로 지정
        transform.position = GameManager.instance.player_init_pos;
        // 정지 애니메이션으로 세팅
        animator.SetTrigger("Stop");
        // 현재 이동방향을 0으로 우선 초기화
        now_dir = 0;
        // 이동 속도 0으로 초기화
        rigidbody2D1.velocity = Vector2.zero;

        // 무적상태 False
        is_Invincibility = false;
        // 플레이어 레이어를 Player로 설정
        gameObject.layer = LayerMask.NameToLayer("Player");
        // 스프라이트의 투명도를 1로 설정 
        Color color = spriteRenderer.color;
        color.a = 1f;
        spriteRenderer.color = color;

        // 점프 제한 횟수 2로 설정(이단 점프)
        limit_jump_num = 2;
        // 현재 점프상태는 아니라고 설정
        is_jump = false;
        GameManager.instance.UpdateLife();
    }

    // 적과의 충돌 시 실행
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 오브젝트의 tag가 Enemy이고 현재 무적상태가 아니라면
        if (collision.gameObject.tag == "Enemy" && !is_Invincibility)
        {
            // 데미지 처리 함수 실행
            Hit();
        }
    }

    // 도착지점 트리거 콜라이더에 들어가면 실행
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 성공 UI 실행
        GameManager.instance.success_window.SetActive(true);
        // 게임은 일시정지
        Time.timeScale = 0f;
    }

    // 게임 시작시 플레이어 초기화
    private void Start()
    {
        Init();
    }
    void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void Update()
    {
        // 플레이어 움직임 감지
        Move();

        // 플레이어 최대 속도 제한(무한 가속 방지)
        LimitSpeed();

        // 키보드 입력 감지
        KeyInputCheck();

        // 플레이어 최대 점프 속도 제한(무한 점프 가속 방지)
        LimitJumpSpeed();

        // 플레이어 이동에 따른 카메라 이동
        Camera_Move();

        // 무적상태 체크
        Invincibility_Check();

        // 점프 후 착지 체크
        Landing_Check();
    }

    // 플레이어 움직임
    public void Move()
    {
        // 키보드 입력에 따라 오른쪽이면 1 왼쪽이면 -1 감지
        float dir = Input.GetAxisRaw("Horizontal");

        // 방향에 따라 플레이어 스프라이트 뒤집기
        // 오른쪽이면 그대로
        if (dir == 1)
        {
            spriteRenderer.flipX = false;
        }
        // 왼쪽이면 뒤집어서 방향 전환
        else if (dir == -1)
        {
            spriteRenderer.flipX = true;
        }

        // 입력된 방향으로 힘을 주어 플레이어 오브젝트 밀기(해당 방향으로 이동)
        rigidbody2D1.AddForce(Vector2.right * dir, ForceMode2D.Impulse);

        // 걷는 애니메이션 실행
        SetWalk(dir);
    }

    // 걷는 애니메이션 실행
    public void SetWalk(float dir)
    {
        // 점프 중이면 실행하지말고 함수 종료
        if (is_jump) return;

        // 현재 이동 방향을 넘겨받은 방향으로 설정
        now_dir = dir;

        // 방향키를 처음 눌렀을 때에만 애니메이션 전환(누르고 있는동안 무한 애니메이션 전환 방지)
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            animator.SetTrigger("Walk");
    }

    // 정지 애니메이션 실행
    public void SetStop()
    {
        animator.SetTrigger("Stop");
    }

    // 점프 애니메이션 실행
    public void SetJump(float dir)
    {
        animator.SetTrigger("Jump");
    }

    // 최대 이동 속도 제한(무한 가속 방지)
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

    // 키보드 입력 감지
    public void KeyInputCheck()
    {
        // 왼쪽, 오른쪽 방향키에서 손을 떘을 경우
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            // 다른 방향키를 누르고 있는 중이 아닐 때
            if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                // 속도를 0으로 세팅하고
                rigidbody2D1.velocity = Vector2.zero;
                // 정지 애니메이션 설정
                SetStop();
            }
        }

        // 스페이스바를 눌렀을 경우
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 점프 제한 횟수가 남았을 경우(2단 점프 후 3단, 4단, ... 무한 점프 방지)
            if (limit_jump_num > 0)
            {
                // 점프 제한 횟수 1감소
                limit_jump_num--;
                // 위쪽 방향으로 설정한 jump_power만큼 힘을 주어 플레이어 오브젝트 밀기(위로 점프)
                rigidbody2D1.AddForce(Vector2.up * jump_power, ForceMode2D.Impulse);
                // 점프 애니메이션 세팅
                SetJump(now_dir);
                // 현재 점프상태를 True로 설정
                is_jump = true;
            }
        }
    }

    // 점프 후 착지 체크
    public void Landing_Check()
    {
        // 현재 점프 제한 수가 2이면 점프 전이므로 함수 즉시 종료
        if (limit_jump_num == 2) return;

        // 위아래 속도를 나타내는 속도 y값이 음수일 경우는 떨어지는 중
        if (rigidbody2D1.velocity.y < 0)
        {
            // 플레이어오브젝트에서 아래 방향으로 레이어가 Platform인 오브젝트들에만 Ray를 쏴서 감지
            RaycastHit2D ray = Physics2D.Raycast(rigidbody2D1.position, Vector2.down, 1, LayerMask.GetMask("Platform"));
            // 감지된 콜라이더가 있으면 플레이어 아래 바닥이 있다는 뜻
            if (ray.collider != null)
            {
                // ray의 길이가 0.65보다 작으면 바닥이 바로 발 아래에 있는 경우
                // 0.65수치는 이미지 크기, 오브젝트 크기에 따라 값이 다름
                if (ray.distance < 0.65f)
                {
                    // 착지 시 방향키를 누르고 있는 중이라면 걷기 애니메이션 실행
                    if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
                        animator.SetTrigger("Walk");
                    // 아니라면 정지 애니메이션 실행
                    else
                        SetStop();

                    // 점프 최대 숫자를 2로 다시 만들기
                    limit_jump_num = 2;
                    // 점프상태 false로 설정
                    is_jump = false;
                }
            }
        }
    }

    // 점프 최대 속도 제한(무한 점프 가속 방지)
    public void LimitJumpSpeed()
    {
        // 속도 값이 넘어가면 설정된 점프력 jump_power 속도로 고정
        if (rigidbody2D1.velocity.y > jump_power)
        {
            rigidbody2D1.velocity = (Vector2.up * jump_power) + (Vector2.right * rigidbody2D1.velocity.x);
        }
    }

    // 플레이어 움직임에 따른 카메라 움직임
    public void Camera_Move()
    {
        // 다음 카메라 위치를 현재 플레이어 위치로 우선 설정
        Vector3 next_cam_pos = transform.position;

        // 다음 카메라 위치의 x값이 왼쪽 끝 벽의 x값+9 보다 작으면 카메라가 왼쪽 벽을 넘어간 경우
        // 왼쪽 벽에 카메라가 닿으면 더이상 왼쪽으로 가지 못해야 하므로
        // 다음 카메라 위치의 x값을 강제로 왼쪽 끝 벽의 x값+9로 고정
        if (next_cam_pos.x < left_end_wall.transform.position.x + 9)
        {
            next_cam_pos.x = left_end_wall.transform.position.x + 9;
        }
        // 다음 카메라 위치의 x값이 오른쪽 끝 벽의 x값-9 보다 크면 카메라가 오른쪽 벽을 넘어간 경우
        // 오른쪽 벽에 카메라가 닿으면 더이상 오른쪽으로 가지 못해야 하므로
        // 다음 카메라 위치의 x값을 강제로 오른쪽 끝 벽의 x값-9로 고정
        else if (next_cam_pos.x > right_end_wall.transform.position.x - 9)
        {
            next_cam_pos.x = right_end_wall.transform.position.x - 9;
        }

        // 다음 카메라 위치의 y값은 플레이어 위치로 맞추면 너무 아래를 비추므로 살짝 위로 올려줘야 한다
        // cam_diff에 적절한 값을 넣어서 맞춰준다(플레이어의 y값이 -2이면 cam_diff는 2로 설정, 플레이어의 y값으 -3이면 cam_diff는 3으로 설정 하는 식으로 하면 맞춰짐)
        next_cam_pos.y += cam_diff;

        // 다음 카메라 위치의 z값은 현재 카메라의 z값으로 설정
        next_cam_pos.z = cam.transform.position.z;
        // 카메라 위치를 다음 카메라 위치로 재설정
        cam.transform.position = next_cam_pos;
    }

    // 무적 상태 체크
    public void Invincibility_Check()
    {
        // 적에게 맞으면 1초간 무적처리
        // 무적동안 반투명하게 깜빡이도록 설정
        // 무적 상태면 플레이어의 레이어를 Invincibility로 설정
        // Invincibility 레이어인 오브젝트는 Enemy와 충돌하지 않게 유니티 프로젝트에 세팅

        // 현재 무적상태 라면
        if (is_Invincibility)
        {
            // 무적 시간 카운트
            invincibility_count += Time.deltaTime;
            // 현재 플레이어 스프라이트의 color값을 가져온다
            Color color = spriteRenderer.color;
            // 무적시간 카운트가 0.25초보다 작은 경우는
            if (invincibility_count < 0.25f)
            {
                // color에서 투명도는 a를 0.4정도로 설정
                color.a = 0.4f;
            }
            // 무적시간 카운트가 0.5초보다 작은 경우는
            else if (invincibility_count < 0.5f)
            {
                // 투명도를 1로
                color.a = 1f;
            }
            // 0.25초보다 작은 경우는
            else if (invincibility_count < 0.75f)
            {
                // 투명도를 0.4로
                color.a = 0.4f;
            }
            // 그 이상일 경우는 투명도 1로 설정
            else
            {
                color.a = 1f;
            }

            // 위에서 설정된 color값을 플레이어 스프라이트에 적용
            spriteRenderer.color = color;

            // 무적시간이 1초가 넘어갔으면
            if (invincibility_count > 1f)
            {
                // 무적을 False로 설정
                is_Invincibility = false;
                // 플레이어의 레이어를 Player로 설정
                gameObject.layer = LayerMask.NameToLayer("Player");
            }
        }
    }

    // 적과 충돌 처리
    public void Hit()
    {
        // 충돌 후 1초간 무적으로 처리
        is_Invincibility = true;
        // 레이어를 Invincibility로 설정
        gameObject.layer = LayerMask.NameToLayer("Invincibility");
        // 무적 상태 카운트 변수를 0초부터 시작하게 0으로 설정
        invincibility_count = 0f;
        // 목숨 수를 1 감소
        now_life--;
        // 목숨 나타내는 이미지 UI 업데이트
        GameManager.instance.UpdateLife();

        // 목숨 수가 0이면
        if (now_life == 0)
        {
            // 재시작 UI 실행후
            GameManager.instance.GameOverCanvas.SetActive(true);
            // 게임 일시정지
            Time.timeScale = 0;
        }
    }
}
