using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public float waitTime = 2f; // Duration of the cutscene

    private void Start()
    {
        StartCoroutine(PlayCutscene());
    }

    private IEnumerator PlayCutscene()
    {
        // Perform cutscene actions here (animations, dialogues, etc.)

        // Wait for the duration of the cutscene
        yield return new WaitForSeconds(waitTime);

        // Load the next scene
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        // Load the next scene
        SceneManager.LoadScene("NextScene"); // Change this to your actual next scene name
    }
}
