using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RestartGameManager : MonoBehaviour
{
    // Restart the game from the first scene (e.g., "Scene1")
    public GameObject GameOver;
    public GameObject GameStop;

    public void RestartFromStart()
    {
        SceneManager.LoadScene("CutScene1"); // Replace "Scene1" with the name of your first scene
        GameOver.SetActive(false);
        GameStop.SetActive(false);
        Time.timeScale = 0;
    }

    // Restart the game from the current active scene
    public void RestartCurrentScene()
    {
        GameObject.FindAnyObjectByType<GameManager>().GameTime = 11;
        GameObject.FindAnyObjectByType<GameManager>().max_life = 3;
        
        Scene currentScene = SceneManager.GetActiveScene(); // Get the current scene
        SceneManager.LoadScene(currentScene.name); // Reload the current scene by name
        for (int i = 0; i < GameObject.FindAnyObjectByType<GameManager>().player.max_life; i++)
        {
            // 목숨 이미지 보이게 설정
            GameObject.FindAnyObjectByType<GameManager>().life[i].enabled = true;
            // 목숨 이미지를 목숨이 있는 이미지로 설정
            GameObject.FindAnyObjectByType<GameManager>().life[i].sprite = GameObject.FindAnyObjectByType<GameManager>().live_flower;
        }
        GameManager.instance.UpdateLife();

        GameOver.SetActive(false);
        GameStop.SetActive(false);
        Time.timeScale = 1f;
    }
}
