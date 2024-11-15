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
        // Trigger jumping sound
        if (Input.GetButtonDown("Jump"))
        {
            PlaySound(jumpSound, false); // Play jump sound once
        }
        // Trigger walking sound
        else if (Input.GetAxis("Horizontal") != 0 && !GetComponent<Player>().is_jump)
        {
            isWalking = true;
            PlaySound(walkSound, true); // Loop walking sound
        }
        else
        {
            isWalking = false;
            audioSource.Stop(); // Stop walking sound when player stops
            AudioSource audioS = GameObject.Find("Running").GetComponent<AudioSource>();
            audioS.Stop();
        }

        
    }

    void PlaySound(AudioClip clip, bool loop)
    {
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.Play();
        AudioSource audioS = GameObject.Find("Running").GetComponent<AudioSource>();
        if (!loop)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
        }
        else if(!audioS.isPlaying)
        {
            
            audioS.clip = clip;
            audioS.loop = loop;
            audioS.Play();

        }
        
       
    }
}
