using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Back2Home : MonoBehaviour
{

    // Start is called before the first frame update
    public void BacktoHome()
    {
        if(GameManager.instance != null)
        {
            GameManager.instance.SetGameUIVisible(false);
            GameManager.instance.GameOverCanvas.SetActive(false);
            GameManager.instance.GameStopCanvas.SetActive(false);
        }

        Time.timeScale = 0f;
        SceneManager.LoadScene("01.StartScene");
    }
}
