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

//UI click 소리 나야되는 버튼에 스크립트 장착하시면 자동으로 소리 나옵니다