using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RestartGameManager : MonoBehaviour
{
    // Restart the game from the first scene (e.g., "Scene1")
    public void RestartFromStart()
    {
        SceneManager.LoadScene("FirstScene"); // Replace "Scene1" with the name of your first scene
    }

    // Restart the game from the current active scene
    public void RestartCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene(); // Get the current scene
        SceneManager.LoadScene(currentScene.name); // Reload the current scene by name
    }
}
