using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement2 : MonoBehaviour
{
    public Transform player;         // 플레이어 객체
    public float moveSpeed = 2f;     // 카메라 이동 속도
    public float maxPlayerXOffset = 3f; // 플레이어가 카메라 중심에서 벗어날 수 있는 최대 X 좌표 **

    private float initialCameraX;    

    void Start()
    {
        initialCameraX = transform.position.x;
    }

    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        // 카메라 기준으로 플레이어의 X 위치 계산
        float playerOffsetX = player.position.x - transform.position.x;

        // 오른쪽
        if (playerOffsetX > maxPlayerXOffset)
        {
            float newCameraX = player.position.x - maxPlayerXOffset;
            transform.position = new Vector3(newCameraX, transform.position.y, transform.position.z);
        }
        // 왼쪽
        else if (playerOffsetX < -maxPlayerXOffset)
        {
            float newCameraX = player.position.x + maxPlayerXOffset;
            transform.position = new Vector3(newCameraX, transform.position.y, transform.position.z);
        }
    }
}