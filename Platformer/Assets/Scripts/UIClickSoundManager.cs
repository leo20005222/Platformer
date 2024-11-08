using UnityEngine;
using UnityEngine.UI;

public class UIButtonSound : MonoBehaviour
{
    private void Awake()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => SoundManager.instance.PlayUIClickSound());
        }
    }
}

//UI click �Ҹ� ���ߵǴ� ��ư�� ��ũ��Ʈ �����Ͻø� �ڵ����� �Ҹ� ���ɴϴ�