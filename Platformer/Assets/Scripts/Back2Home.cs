using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Back2Home : MonoBehaviour
{
    // Start is called before the first frame update
    public void BacktoHome()
    {
        SceneManager.LoadScene("01.StartScene");
    }
}
