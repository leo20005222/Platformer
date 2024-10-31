using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    public AudioClip walkSound; // Drag your walk sound here in the Inspector
    public AudioClip jumpSound; // Drag your jump sound here in the Inspector
    private AudioSource audioSource;
    private bool isWalking = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Trigger walking sound
        if (Input.GetAxis("Horizontal") != 0 && !isWalking)
        {
            isWalking = true;
            PlaySound(walkSound, true); // Loop walking sound
        }
        else if (Input.GetAxis("Horizontal") == 0 && isWalking)
        {
            isWalking = false;
            audioSource.Stop(); // Stop walking sound when player stops
        }

        // Trigger jumping sound
        if (Input.GetButtonDown("Jump"))
        {
            PlaySound(jumpSound, false); // Play jump sound once
        }
    }

    void PlaySound(AudioClip clip, bool loop)
    {
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.Play();
    }
}
