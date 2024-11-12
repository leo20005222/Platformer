using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioMixer audioMixer; // Reference to the Master Audio Mixer
    public Slider volumeSlider;   // Reference to the UI volume slider

    private void Start()
    {
        // Load the saved volume setting, default to 1.0 (100%) if no value is saved
        float savedVolume = PlayerPrefs.GetFloat("volume", 1f);

        // Set the slider's value to the saved volume
        volumeSlider.value = savedVolume;

        // Apply the volume setting to the audio mixer
        SetVolume(savedVolume);

        // Add a listener to the slider to call SetVolume when the value changes
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    // Method to set the volume of the Master Mixer
    public void SetVolume(float volume)
    {
        // Convert the linear volume (0.0001 to 1) to a logarithmic scale (-80 to 0 dB)
        float dBVolume = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20;

        // Set the volume on the Master Mixer
        audioMixer.SetFloat("MasterVolume", dBVolume);

        // Save the current volume setting using PlayerPrefs
        PlayerPrefs.SetFloat("volume", volume);
    }
}
