using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;         // 플레이어 객체
    public GameObject targetObject;  // 화면 밖 체크할 오브젝트
    public float moveSpeed = 2f;     // 카메라가 자동으로 오른쪽으로 움직이는 속도
    public float maxPlayerXOffset = 4.5f; // 플레이어가 카메라 중심에서 벗어날 수 있는 최대 X 좌표
    public Camera mainCamera;        // 카메라의 뷰포트 사용을 위해 필요
    private float initialCameraX;

    void Start()
    {
        initialCameraX = transform.position.x;  // 초기 카메라 X 위치를 저장합니다.
    }

    void Update()
    {
        MoveCameraRight();
        FollowPlayer();
        CheckIfObjectIsOutOfView();
    }

    // 카메라를 천천히 오른쪽으로 이동시키는 함수
    void MoveCameraRight()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }

    // 플레이어가 오른쪽으로 이동할 때만 따라가는 함수
    void FollowPlayer()
    {
        float playerOffsetX = player.position.x - transform.position.x;

        // 플레이어가 오른쪽에서 maxPlayerXOffset 이상 벗어났을 때만 카메라가 따라감
        if (playerOffsetX > maxPlayerXOffset)
        {
            float newCameraX = player.position.x - maxPlayerXOffset;
            transform.position = new Vector3(newCameraX, transform.position.y, transform.position.z);
        }
    }

    // 오브젝트가 화면 밖으로 나갔는지 확인하는 함수
    void CheckIfObjectIsOutOfView()
    {
        if (IsOutOfView(targetObject))
        {
            Debug.Log("오브젝트가 화면 밖으로 나갔습니다.");
        }
    }

    // 특정 오브젝트가 카메라 뷰포트 밖으로 나갔는지 체크하는 함수
    bool IsOutOfView(GameObject entity)
    {
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(entity.transform.position);
        return viewportPos.x < 0 || viewportPos.x > 1 || viewportPos.y < 0 || viewportPos.y > 1;
    }
}
