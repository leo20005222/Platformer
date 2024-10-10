using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private float GameTime = 11;
    public Text GameTimeText;
    public GameObject GameOverCanvas;
    public GameObject GameStopCanvas;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameTime -= Time.deltaTime;
        GameTimeText.text = "Time: " + (int)GameTime;
        if ((int)GameTime == 0)
        {
            GameOverCanvas.SetActive(true);
            Time.timeScale = 0f;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
            {
                GameStopCanvas.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                GameStopCanvas.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
}
