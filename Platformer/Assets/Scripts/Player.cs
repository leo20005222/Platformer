using UnityEngine;

public class Player : MonoBehaviour
{
    public static GameManager Instance;
    // �ΰ��� ���� ī�޶�
    public Camera cam;
    // �÷��̾� Position y���� ī�޶��� Position y�� ����
    public float cam_diff;

    // �� ���� �� ��
    public GameObject left_end_wall;
    // �� ������ �� ��
    public GameObject right_end_wall;

    // �÷��̾��� Rigidbocy
    public Rigidbody2D rigidbody2D1;

    // �÷��̾��� �ȴ� �ӵ�
    public float walk_speed;
    // �÷��̾��� ������
    public float jump_power;

    // ���� �������� ����
    bool is_Invincibility;
    // ���� �ð� ī��Ʈ ����
    float invincibility_count;

    // �÷��̾��� ��������Ʈ ������
    public SpriteRenderer spriteRenderer;
    // �÷��̾��� �ִϸ�����
    public Animator animator;
    // �÷��̾��� ���� �̵�����
    public float now_dir;

    // ���� ���� Ƚ��
    public int limit_jump_num;
    // ���� ���������� ����
    public bool is_jump;

    // �ִ� ��� ��
    public int max_life;
    // ���� ��� ��
    public int now_life;

    // ���� �� �÷��̾� �ʱ�ȭ �Լ�
    public void Init()
    {
        // ���� ��� ���� �ִ�ġ�� ����
        now_life = max_life;
        // ó�� ���� ��ġ�� ����
        transform.position = GameManager.instance.player_init_pos;
        // ���� �ִϸ��̼����� ����
        animator.SetTrigger("Stop");
        // ���� �̵������� 0���� �켱 �ʱ�ȭ
        now_dir = 0;
        // �̵� �ӵ� 0���� �ʱ�ȭ
        rigidbody2D1.velocity = Vector2.zero;

        // �������� False
        is_Invincibility = false;
        // �÷��̾� ���̾ Player�� ����
        gameObject.layer = LayerMask.NameToLayer("Player");
        // ��������Ʈ�� ������ 1�� ���� 
        Color color = spriteRenderer.color;
        color.a = 1f;
        spriteRenderer.color = color;

        // ���� ���� Ƚ�� 2�� ����(�̴� ����)
        limit_jump_num = 2;
        // ���� �������´� �ƴ϶�� ����
        is_jump = false;
        GameManager.instance.UpdateLife();
    }

    // ������ �浹 �� ����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹�� ������Ʈ�� tag�� Enemy�̰� ���� �������°� �ƴ϶��
        if (collision.gameObject.tag == "Enemy" && !is_Invincibility)
        {
            // ������ ó�� �Լ� ����
            Hit();
        }
    }

    // �������� Ʈ���� �ݶ��̴��� ���� ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� UI ����
        GameManager.instance.success_window.SetActive(true);
        // ������ �Ͻ�����
        Time.timeScale = 0f;
    }

    // ���� ���۽� �÷��̾� �ʱ�ȭ
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
        // �÷��̾� ������ ����
        Move();

        // �÷��̾� �ִ� �ӵ� ����(���� ���� ����)
        LimitSpeed();

        // Ű���� �Է� ����
        KeyInputCheck();

        // �÷��̾� �ִ� ���� �ӵ� ����(���� ���� ���� ����)
        LimitJumpSpeed();

        // �÷��̾� �̵��� ���� ī�޶� �̵�
        Camera_Move();

        // �������� üũ
        Invincibility_Check();

        // ���� �� ���� üũ
        Landing_Check();
    }

    // �÷��̾� ������
    public void Move()
    {
        // Ű���� �Է¿� ���� �������̸� 1 �����̸� -1 ����
        float dir = Input.GetAxisRaw("Horizontal");

        // ���⿡ ���� �÷��̾� ��������Ʈ ������
        // �������̸� �״��
        if (dir == 1)
        {
            spriteRenderer.flipX = false;
        }
        // �����̸� ����� ���� ��ȯ
        else if (dir == -1)
        {
            spriteRenderer.flipX = true;
        }

        // �Էµ� �������� ���� �־� �÷��̾� ������Ʈ �б�(�ش� �������� �̵�)
        rigidbody2D1.AddForce(Vector2.right * dir, ForceMode2D.Impulse);

        // �ȴ� �ִϸ��̼� ����
        SetWalk(dir);
    }

    // �ȴ� �ִϸ��̼� ����
    public void SetWalk(float dir)
    {
        // ���� ���̸� ������������ �Լ� ����
        if (is_jump) return;

        // ���� �̵� ������ �Ѱܹ��� �������� ����
        now_dir = dir;

        // ����Ű�� ó�� ������ ������ �ִϸ��̼� ��ȯ(������ �ִµ��� ���� �ִϸ��̼� ��ȯ ����)
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            animator.SetTrigger("Walk");
    }

    // ���� �ִϸ��̼� ����
    public void SetStop()
    {
        animator.SetTrigger("Stop");
    }

    // ���� �ִϸ��̼� ����
    public void SetJump(float dir)
    {
        animator.SetTrigger("Jump");
    }

    // �ִ� �̵� �ӵ� ����(���� ���� ����)
    public void LimitSpeed()
    {
        // �¿� �����̴� �ӵ� x���� �ִ� �ӵ����� ũ��
        if (rigidbody2D1.velocity.x > walk_speed)
        {
            // x�ӵ��� �ִ� �ӵ� ������ ����
            // y�ӵ��� ���� �ӵ� ������ �״��
            rigidbody2D1.velocity = (Vector2.right * walk_speed) + (Vector2.up * rigidbody2D1.velocity.y);
        }
        // �������� �����̴� �ӵ����� ������ ǥ��
        // �¿� �����̴� �ӵ� x���� (�ִ� �ӵ��� * -1) ���� ������
        else if (rigidbody2D1.velocity.x < walk_speed * -1)
        {
            // x�ӵ��� �ִ� �ӵ� ������ ����, ������ ��������
            // y�ӵ��� ���� �ӵ� ������ �״��
            rigidbody2D1.velocity = (Vector2.left * walk_speed) + (Vector2.up * rigidbody2D1.velocity.y);
        }
    }

    // Ű���� �Է� ����
    public void KeyInputCheck()
    {
        // ����, ������ ����Ű���� ���� ���� ���
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            // �ٸ� ����Ű�� ������ �ִ� ���� �ƴ� ��
            if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                // �ӵ��� 0���� �����ϰ�
                rigidbody2D1.velocity = Vector2.zero;
                // ���� �ִϸ��̼� ����
                SetStop();
            }
        }

        // �����̽��ٸ� ������ ���
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // ���� ���� Ƚ���� ������ ���(2�� ���� �� 3��, 4��, ... ���� ���� ����)
            if (limit_jump_num > 0)
            {
                // ���� ���� Ƚ�� 1����
                limit_jump_num--;
                // ���� �������� ������ jump_power��ŭ ���� �־� �÷��̾� ������Ʈ �б�(���� ����)
                rigidbody2D1.AddForce(Vector2.up * jump_power, ForceMode2D.Impulse);
                // ���� �ִϸ��̼� ����
                SetJump(now_dir);
                // ���� �������¸� True�� ����
                is_jump = true;
            }
        }
    }

    // ���� �� ���� üũ
    public void Landing_Check()
    {
        // ���� ���� ���� ���� 2�̸� ���� ���̹Ƿ� �Լ� ��� ����
        if (limit_jump_num == 2) return;

        // ���Ʒ� �ӵ��� ��Ÿ���� �ӵ� y���� ������ ���� �������� ��
        if (rigidbody2D1.velocity.y < 0)
        {
            // �÷��̾������Ʈ���� �Ʒ� �������� ���̾ Platform�� ������Ʈ�鿡�� Ray�� ���� ����
            RaycastHit2D ray = Physics2D.Raycast(rigidbody2D1.position, Vector2.down, 1, LayerMask.GetMask("Platform"));
            // ������ �ݶ��̴��� ������ �÷��̾� �Ʒ� �ٴ��� �ִٴ� ��
            if (ray.collider != null)
            {
                // ray�� ���̰� 0.65���� ������ �ٴ��� �ٷ� �� �Ʒ��� �ִ� ���
                // 0.65��ġ�� �̹��� ũ��, ������Ʈ ũ�⿡ ���� ���� �ٸ�
                if (ray.distance < 0.65f)
                {
                    // ���� �� ����Ű�� ������ �ִ� ���̶�� �ȱ� �ִϸ��̼� ����
                    if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
                        animator.SetTrigger("Walk");
                    // �ƴ϶�� ���� �ִϸ��̼� ����
                    else
                        SetStop();

                    // ���� �ִ� ���ڸ� 2�� �ٽ� �����
                    limit_jump_num = 2;
                    // �������� false�� ����
                    is_jump = false;
                }
            }
        }
    }

    // ���� �ִ� �ӵ� ����(���� ���� ���� ����)
    public void LimitJumpSpeed()
    {
        // �ӵ� ���� �Ѿ�� ������ ������ jump_power �ӵ��� ����
        if (rigidbody2D1.velocity.y > jump_power)
        {
            rigidbody2D1.velocity = (Vector2.up * jump_power) + (Vector2.right * rigidbody2D1.velocity.x);
        }
    }

    // �÷��̾� �����ӿ� ���� ī�޶� ������
    public void Camera_Move()
    {
        // ���� ī�޶� ��ġ�� ���� �÷��̾� ��ġ�� �켱 ����
        Vector3 next_cam_pos = transform.position;

        // ���� ī�޶� ��ġ�� x���� ���� �� ���� x��+9 ���� ������ ī�޶� ���� ���� �Ѿ ���
        // ���� ���� ī�޶� ������ ���̻� �������� ���� ���ؾ� �ϹǷ�
        // ���� ī�޶� ��ġ�� x���� ������ ���� �� ���� x��+9�� ����
        if (next_cam_pos.x < left_end_wall.transform.position.x + 9)
        {
            next_cam_pos.x = left_end_wall.transform.position.x + 9;
        }
        // ���� ī�޶� ��ġ�� x���� ������ �� ���� x��-9 ���� ũ�� ī�޶� ������ ���� �Ѿ ���
        // ������ ���� ī�޶� ������ ���̻� ���������� ���� ���ؾ� �ϹǷ�
        // ���� ī�޶� ��ġ�� x���� ������ ������ �� ���� x��-9�� ����
        else if (next_cam_pos.x > right_end_wall.transform.position.x - 9)
        {
            next_cam_pos.x = right_end_wall.transform.position.x - 9;
        }

        // ���� ī�޶� ��ġ�� y���� �÷��̾� ��ġ�� ���߸� �ʹ� �Ʒ��� ���߹Ƿ� ��¦ ���� �÷���� �Ѵ�
        // cam_diff�� ������ ���� �־ �����ش�(�÷��̾��� y���� -2�̸� cam_diff�� 2�� ����, �÷��̾��� y���� -3�̸� cam_diff�� 3���� ���� �ϴ� ������ �ϸ� ������)
        next_cam_pos.y += cam_diff;

        // ���� ī�޶� ��ġ�� z���� ���� ī�޶��� z������ ����
        next_cam_pos.z = cam.transform.position.z;
        // ī�޶� ��ġ�� ���� ī�޶� ��ġ�� �缳��
        cam.transform.position = next_cam_pos;
    }

    // ���� ���� üũ
    public void Invincibility_Check()
    {
        // ������ ������ 1�ʰ� ����ó��
        // �������� �������ϰ� �����̵��� ����
        // ���� ���¸� �÷��̾��� ���̾ Invincibility�� ����
        // Invincibility ���̾��� ������Ʈ�� Enemy�� �浹���� �ʰ� ����Ƽ ������Ʈ�� ����

        // ���� �������� ���
        if (is_Invincibility)
        {
            // ���� �ð� ī��Ʈ
            invincibility_count += Time.deltaTime;
            // ���� �÷��̾� ��������Ʈ�� color���� �����´�
            Color color = spriteRenderer.color;
            // �����ð� ī��Ʈ�� 0.25�ʺ��� ���� ����
            if (invincibility_count < 0.25f)
            {
                // color���� ������ a�� 0.4������ ����
                color.a = 0.4f;
            }
            // �����ð� ī��Ʈ�� 0.5�ʺ��� ���� ����
            else if (invincibility_count < 0.5f)
            {
                // ������ 1��
                color.a = 1f;
            }
            // 0.25�ʺ��� ���� ����
            else if (invincibility_count < 0.75f)
            {
                // ������ 0.4��
                color.a = 0.4f;
            }
            // �� �̻��� ���� ���� 1�� ����
            else
            {
                color.a = 1f;
            }

            // ������ ������ color���� �÷��̾� ��������Ʈ�� ����
            spriteRenderer.color = color;

            // �����ð��� 1�ʰ� �Ѿ����
            if (invincibility_count > 1f)
            {
                // ������ False�� ����
                is_Invincibility = false;
                // �÷��̾��� ���̾ Player�� ����
                gameObject.layer = LayerMask.NameToLayer("Player");
            }
        }
    }

    // ���� �浹 ó��
    public void Hit()
    {
        // �浹 �� 1�ʰ� �������� ó��
        is_Invincibility = true;
        // ���̾ Invincibility�� ����
        gameObject.layer = LayerMask.NameToLayer("Invincibility");
        // ���� ���� ī��Ʈ ������ 0�ʺ��� �����ϰ� 0���� ����
        invincibility_count = 0f;
        // ��� ���� 1 ����
        now_life--;
        // ��� ��Ÿ���� �̹��� UI ������Ʈ
        GameManager.instance.UpdateLife();

        // ��� ���� 0�̸�
        if (now_life == 0)
        {
            // ����� UI ������
            GameManager.instance.GameOverCanvas.SetActive(true);
            // ���� �Ͻ�����
            Time.timeScale = 0;
        }
    }
}
