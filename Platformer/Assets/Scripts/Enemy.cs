using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // �� ������Ʈ�� RigidBody
    public Rigidbody2D rigidbody2D1;
    // �� ������Ʈ�� �̵��ӵ�
    public float walk_speed;

    // ���� �̵��ϴ� ����� ���� ���� �� �ٴ� ������Ʈ
    public GameObject left_end_point;
    // ���� �̵��ϴ� ����� ���� ������ �� �ٴ� ������Ʈ
    public GameObject right_end_point;

    // �� ������Ʈ�� ��������Ʈ ������
    public SpriteRenderer spriteRenderer;

    // ���� ���� ������ ���� �ִ��� ����
    bool is_left;
    // �� �̵�����(���� -1, ������ 1)
    float dir;

    private void Start()
    {
        // ���� �� ���ʹ������� ����
        is_left = true;
        dir = -1;
    }

    private void Update()
    {
        // ���� �̵� �������� �� ������Ʈ�� ���� �־� �б�(�̵�)
        rigidbody2D1.AddForce(Vector2.right * dir, ForceMode2D.Impulse);
        // �� �̵��ӵ� ���� ���� ����
        LimitSpeed();

        // �������� �̵����̾��� ���� �� ��ġ�� x���� ���� �� ������Ʈ�� x������ ������ �̵������ ���� ���� �����ߴٴ� �ǹ�
        if (transform.position.x <= left_end_point.transform.position.x && is_left)
        {
            // �� ������Ʈ�� �ӵ��� 0���� ����� ���߰�
            rigidbody2D1.velocity = Vector2.zero;

            // ������ ���������� ����
            is_left = false;
            dir = 1;

        }
        // ���������� �̵����̾��� ���� �� ��ġ�� x���� ������ �� ������Ʈ�� x������ ũ�� �̵������ ������ ���� �����ߴٴ� �ǹ�
        else if (transform.position.x >= right_end_point.transform.position.x && !is_left)
        {
            // �� ������Ʈ�� �ӵ��� 0���� ����� ���߰�
            rigidbody2D1.velocity = Vector2.zero;

            // ������ �������� ����
            is_left = true;
            dir = -1;

        }
        if (dir == 1)
        {
            spriteRenderer.flipX = false;
        }
        // �����̸� ����� ���� ��ȯ
        else if (dir == -1)
        {
            spriteRenderer.flipX = true;
        }
    }

    // �� �̵��ӵ� ���� ���� ����
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
}
