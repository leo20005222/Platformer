using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;

    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("volume", 1f); // Default to 1 if no value is saved
        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("volume", volume); // Save the volume setting
    }
}
