using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource flyingSource;

    [Header("Clips")]
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip flyingLoop;
    [SerializeField] private AudioClip hitSfx;
    [SerializeField] private AudioClip scoreSfx;

    [Header("Options")]
    [SerializeField] private bool playMusicOnStart = true;

    private void OnEnable()
    {
        AudioEvents.GameStarted += HandleGameStarted;
        AudioEvents.GameOver += HandleGameOver;
        AudioEvents.BirdFlyStart += HandleBirdFlyStart;
        AudioEvents.BirdFlyStop += HandleBirdFlyStop;
        AudioEvents.BirdHit += HandleBirdHit;
        AudioEvents.Score += HandleScore;
    }

    private void OnDisable()
    {
        AudioEvents.GameStarted -= HandleGameStarted;
        AudioEvents.GameOver -= HandleGameOver;
        AudioEvents.BirdFlyStart -= HandleBirdFlyStart;
        AudioEvents.BirdFlyStop -= HandleBirdFlyStop;
        AudioEvents.BirdHit -= HandleBirdHit;
        AudioEvents.Score -= HandleScore;
    }

    private void Start()
    {
        if (playMusicOnStart)
        {
            PlayMusic();
        }
    }

    private void HandleGameStarted()
    {
        PlayMusic();
    }

    private void HandleGameOver()
    {
        StopFlyingLoop();
    }

    private void HandleBirdFlyStart()
    {
        PlayFlapSfx();
    }

    private void HandleBirdFlyStop()
    {
        StopFlyingLoop();
    }

    private void HandleBirdHit()
    {
        PlaySfx(hitSfx);
    }

    private void HandleScore()
    {
        PlaySfx(scoreSfx);
    }

    private void PlayMusic()
    {
        if (musicSource == null || backgroundMusic == null) return;
        if (musicSource.isPlaying) return;
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    private void PlayFlapSfx()
    {
        if (flyingLoop == null) return;

        if (flyingSource != null)
        {
            flyingSource.PlayOneShot(flyingLoop);
            return;
        }

        PlaySfx(flyingLoop);
    }

    private void StopFlyingLoop()
    {
        if (flyingSource == null) return;
        if (!flyingSource.isPlaying) return;
        flyingSource.Stop();
    }

    private void PlaySfx(AudioClip clip)
    {
        if (sfxSource == null || clip == null) return;
        sfxSource.PlayOneShot(clip);
    }
}