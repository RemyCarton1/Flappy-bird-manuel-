using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource audioSourceTheme;
    public AudioSource audioSourceSFXJump;
    public AudioSource audioSourceSFXButton;
    public AudioSource audioSourceSFXPoint;

    [Header("Music Clips")]
    public AudioClip menuTheme;
    public AudioClip gameTheme;
    public AudioClip gameThemeHard; // 💥 Nueva música para score >= 10
    public AudioClip gameOverTheme;

    private bool hardModeActive = false;

    private void Awake()
    {
        audioSourceTheme.clip = menuTheme;
        audioSourceTheme.loop = true;
        audioSourceTheme.Play();
    }

    private void Update()
    {
        // Salto
        if (GameController.instance.canPlay && Input.GetKeyUp(KeyCode.Space))
        {
            audioSourceSFXJump.Stop();
            audioSourceSFXJump.Play();
        }
    }

    public void PointSFX()
    {
        audioSourceSFXPoint.PlayOneShot(audioSourceSFXPoint.clip);
    }

    public void ButtonSFX()
    {
        audioSourceSFXButton.PlayOneShot(audioSourceSFXButton.clip);
    }

    public void GameThemeMusic()
    {
        hardModeActive = false;
        audioSourceTheme.Stop();
        audioSourceTheme.clip = gameTheme;
        audioSourceTheme.loop = true;
        audioSourceTheme.Play();
    }

    public void GameThemeHardMusic()
    {
        if (hardModeActive) return; // Evita reiniciar si ya está activa
        hardModeActive = true;
        audioSourceTheme.Stop();
        audioSourceTheme.clip = gameThemeHard;
        audioSourceTheme.loop = true;
        audioSourceTheme.Play();
    }

    public void GameOverMusic()
    {
        hardModeActive = false;
        audioSourceTheme.Stop();
        audioSourceTheme.clip = gameOverTheme;
        audioSourceTheme.loop = false;
        audioSourceTheme.Play();
    }

    public void MenuMusic()
    {
        hardModeActive = false;
        audioSourceTheme.Stop();
        audioSourceTheme.clip = menuTheme;
        audioSourceTheme.loop = false;
        audioSourceTheme.Play();
    }

    public void StopAllSFX()
    {
        audioSourceSFXJump.Stop();
        audioSourceSFXPoint.Stop();
    }
}
