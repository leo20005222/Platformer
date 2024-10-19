using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //scene switcher

public class SceneSwitch : MonoBehaviour
{
    public string Cutscene;
    public string NextScene;

    private void OnTriggerEnter2D(Collider2D collision) // Detect the player when entering the trigger zone
    {
        if (collision.CompareTag("Player")) // Check if the object that collided is tagged "Player"
        {
            StartCoroutine(LoadCutscene()); // Start loading the cutscene
        }
    }

    private IEnumerator LoadCutscene()
    {
        // Load the cutscene scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(Cutscene, LoadSceneMode.Single);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Once cutscene is finished, load the next scene
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        // Load the next scene
        SceneManager.LoadScene(NextScene);
    }
}
