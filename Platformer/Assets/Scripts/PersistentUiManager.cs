using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentUiManager : MonoBehaviour
{
    private static PersistentUiManager instance;

    private void Awake()
    {
        // Check if there's already an instance of this script
        if (instance != null)
        {
            Destroy(gameObject); // If there's an existing one, destroy this duplicate
            return;
        }

        // Otherwise, set this as the instance and make it persistent across scenes
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
