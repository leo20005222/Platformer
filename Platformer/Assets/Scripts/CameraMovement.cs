using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject targetEntity;  // 감지할 엔티티
    public float moveSpeed = 2f;     // 카메라 이동 속도

    public Camera mainCamera; //카메라의 시점 추적을 위해서 사용

    void Start()
    {

    }

    void Update()
    {
        // 카메라를 오른쪽으로 천천히 이동
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;


        // 엔티티가 화면 밖으로 나갔는지 체크
        if (IsOutOfView(targetEntity))
        {
            //플래이어의 체력을 깎는 함수 사용
            Debug.Log("밖으로 나감 : 데미직 받는 함수 쓰기");
        }
    }

    // 엔티티가 카메라 뷰포트 밖으로 나갔는지 체크하는 함수
    bool IsOutOfView(GameObject entity)
    {
        // 엔티티의 위치를 뷰포트 좌표계로 변환(카메라 시점 기준 위치)
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(entity.transform.position);

        // 뷰포트 좌표계에서 화면 밖으로 나갔을때 x, y가 0~1 범위를 벗어남
        if (viewportPos.x < 0 || viewportPos.x > 1 || viewportPos.y < 0 || viewportPos.y > 1)
        {
            return true;
        }
        return false;
    }
}