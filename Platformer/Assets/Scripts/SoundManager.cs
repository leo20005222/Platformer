using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource uiClickSource;
    public AudioSource stageClearSource;
    public AudioSource endingSource;
    public AudioSource mainLobbySource;
    public AudioSource[] stageBackgroundSources; // Array for stage background sounds
    public AudioSource hpMinusSource3;
    public AudioSource hpMinusSource2;
    public AudioSource hpLowSource;
    public AudioSource gameOverSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist this GameObject across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayUIClickSound() => uiClickSource.Play();
    public void PlayStageClearSound() => stageClearSource.Play();
    public void PlayEndingSound() => endingSource.Play();
    public void PlayMainLobbySound() => mainLobbySource.Play();
    public void PlayStageBackgroundSound(int stageIndex)
    {
        if (stageIndex >= 0 && stageIndex < stageBackgroundSources.Length)
        {
            stageBackgroundSources[stageIndex].Play();
        }
    }
    public void PlayHpMinusSound3() => hpMinusSource3.Play();
    public void PlayHpMinusSound2() => hpMinusSource2.Play();
    public void PlayHpLowSound() => hpLowSource.Play();
    public void PlayGameOverSound() => gameOverSource.Play();
}
