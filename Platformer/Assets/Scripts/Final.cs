using UnityEngine;

public class CharacterCollision : MonoBehaviour
{
    // �浹 �� ǥ���� UI �г�
    public GameObject uiPanel;
    // �浹�� ����� �±� (������ �±�)
    public string targetTag = "LastMan"; // ��: "Man" �±�

    void Start()
    {
        // UI �г��� �ʱ⿡�� ����ϴ�.
        if (uiPanel != null)
        {
            uiPanel.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹�� ������Ʈ�� �±װ� ������ �±׿� ��ġ�ϴ��� Ȯ��
        if (collision.gameObject.CompareTag(targetTag))
        {
            // UI �г��� Ȱ��ȭ�Ͽ� ǥ��
            if (uiPanel != null)
            {
                uiPanel.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
}
