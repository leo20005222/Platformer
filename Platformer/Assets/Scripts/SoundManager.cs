using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Singleton instance
    public static SoundManager instance;

    // Audio Source to play the clips
    public AudioSource audioSource;

    // AudioClips
    public AudioClip uiClickClip;
    public AudioClip stageClearClip;
    public AudioClip loseHealthClip;
    public AudioClip hpLowClip;
    public AudioClip gameOverClip;

    private void Awake()
    {
        // Singleton pattern to maintain one SoundManager instance
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to play a given AudioClip
    public void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("SoundManager: Missing AudioClip or AudioSource.");
        }
    }

    // Specific methods to play individual sounds
    public void PlayUIClickSound() => PlaySound(uiClickClip);

    public void PlayStageClearSound() => PlaySound(stageClearClip);

    public void PlayLoseHealthSound() => PlaySound(loseHealthClip);

    public void PlayHpLowSound() => PlaySound(hpLowClip);

    public void PlayGameOverSound() => PlaySound(gameOverClip);
}
