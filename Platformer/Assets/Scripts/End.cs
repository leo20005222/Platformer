using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class End : MonoBehaviour
{
    public GameObject uiCanvas;
    public GameObject uiCanvas1;
    // Start is called before the first frame update
    void Start()
    {
        if (uiCanvas != null)
        {
            Canvas[] canvases = uiCanvas.GetComponentsInChildren<Canvas>();
            foreach (Canvas canvas in canvases)
            {
                canvas.sortingLayerName = "UI";
                canvas.sortingOrder = 100; // 맨 앞쪽으로 설정
            }
        }
        if (uiCanvas1 != null)
        {
            Canvas[] canvases = uiCanvas1.GetComponentsInChildren<Canvas>();
            foreach (Canvas canvas in canvases)
            {
                canvas.sortingLayerName = "UI";
                canvas.sortingOrder = 100; // 맨 앞쪽으로 설정
            }
        }
    }
}
