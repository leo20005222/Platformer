using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrivalSound : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player has entered the ArrivalSpot
        if (collision.CompareTag("Player"))
        {
            // Play the StageClear sound
            SoundManager.instance.PlayStageClearSound();
            Debug.Log("Stage Clear!");

            // You can add code here to load the next scene or show the stage clear UI
        }
    }
}
