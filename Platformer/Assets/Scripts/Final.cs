using UnityEngine;

public class CharacterCollision : MonoBehaviour
{
    // 충돌 시 표시할 UI 패널
    public GameObject uiPanel;
    // 충돌할 대상의 태그 (남자의 태그)
    public string targetTag = "LastMan"; // 예: "Man" 태그

    void Start()
    {
        // UI 패널을 초기에는 숨깁니다.
        if (uiPanel != null)
        {
            uiPanel.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 오브젝트의 태그가 지정된 태그와 일치하는지 확인
        if (collision.gameObject.CompareTag(targetTag))
        {
            // UI 패널을 활성화하여 표시
            if (uiPanel != null)
            {
                uiPanel.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
}
