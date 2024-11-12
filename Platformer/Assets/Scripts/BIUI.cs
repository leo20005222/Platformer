using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BIUI : MonoBehaviour
{
    public SpriteRenderer backgroundRenderer;
    public SpriteRenderer backgroundRenderer1;
    public SpriteRenderer backgroundRenderer2;
    public SpriteRenderer backgroundRenderer3;
    public Canvas uiCanvas1;
    public Canvas uiCanvas;
    // Start is called before the first frame update
    void Start()
    {
        if (backgroundRenderer != null)
        {
            backgroundRenderer.sortingLayerName = "Background";
            backgroundRenderer.sortingOrder = 0; // 맨 뒤로 설정
        }
        if(backgroundRenderer1 != null)
        {
            backgroundRenderer1.sortingLayerName = "Background";
            backgroundRenderer1.sortingOrder = 0; // 맨 뒤로 설정
        }
        if (backgroundRenderer2 != null)
        {
            backgroundRenderer2.sortingLayerName = "Background";
            backgroundRenderer2.sortingOrder = 0; // 맨 뒤로 설정
        }
        if (backgroundRenderer3 != null)
        {
            backgroundRenderer3.sortingLayerName = "Background";
            backgroundRenderer3.sortingOrder = 0; // 맨 뒤로 설정
        }
        // UI 캔버스의 Sorting Layer 및 Order 설정
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
