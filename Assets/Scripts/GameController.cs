using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public PlayerController playerController;
    public AudioController audioController;

    [Header("Paneles")]
    public GameObject StartPanel;
    public GameObject gameOverPanel;
    public GameObject ScorePanel;
    public GameObject InfoPanel; // 🆕 Panel de información adicional

    public bool canPlay = false;
    public bool gameOver = false;
    public bool isPaused = false;

    public GameObject pipesParent;
    public Button playButton;
    public Button playButton2;
    public Button playButton3;

    public Button pauseButton;

    [Header("Sprites del botón Pausa/Play")]
    public Sprite pauseSprite;
    public Sprite playSprite;

    private Image pauseButtonImage;
    private float restartRequestTime = -1f;

    // 🆕 Botón del panel de información
    public Button closeInfoButton;

    private void Awake()
    {
        instance = this;

        canPlay = false;
        Time.timeScale = 0f;

        StartPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        InfoPanel.SetActive(false); // 🆕 Aseguramos que esté oculto al inicio

        if (pauseButton != null)
            pauseButton.gameObject.SetActive(false);

        // 🎮 Asignar botones de inicio
        if (playButton != null)
            playButton.onClick.AddListener(StartGame);
        if (playButton2 != null)
            playButton2.onClick.AddListener(StartGame);
        if (playButton3 != null)
            playButton3.onClick.AddListener(StartGame);

        // ⏸️ Configurar botón de pausa
        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(TogglePause);
            pauseButtonImage = pauseButton.GetComponent<Image>();
        }

        // 🆕 Botón para cerrar el panel de información
        if (closeInfoButton != null)
            closeInfoButton.onClick.AddListener(CloseInfoPanel);
    }

    private void StartGame()
    {
        canPlay = true;
        playerController.ToggleRigidBody();
        Time.timeScale = 1f;

        StartPanel.SetActive(false);
        InfoPanel.SetActive(false);
        ScorePanel.SetActive(true);

        playButton.gameObject.SetActive(false);
        playButton2.gameObject.SetActive(false);
        playButton3.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);

        if (pauseButtonImage != null && pauseSprite != null)
            pauseButtonImage.sprite = pauseSprite;

        // 🎵 Inicia la música del juego
        audioController.GameThemeMusic();
    }

    private void Update()
    {
        // Reinicio diferido (para efecto del botón)
        if (restartRequestTime > 0f && Time.unscaledTime - restartRequestTime >= 0.5f)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            // ▶️ Reanudar
            isPaused = false;
            Time.timeScale = 1f;
            canPlay = true;

            if (pauseButtonImage != null && pauseSprite != null)
                pauseButtonImage.sprite = pauseSprite;
        }
        else
        {
            // ⏸️ Pausar
            isPaused = true;
            Time.timeScale = 0f;
            canPlay = false;

            audioController.StopAllSFX();

            if (pauseButtonImage != null && playSprite != null)
                pauseButtonImage.sprite = playSprite;
        }
    }

    public void ToggleCanPlay()
    {
        if (canPlay)
        {
            canPlay = false;
            playerController.ToggleRigidBody();
        }
        else
        {
            canPlay = true;
            playerController.ToggleRigidBody();
        }
    }

    public void CallGameOver()
    {
        gameOverPanel.SetActive(true);
        ScorePanel.SetActive(false);

        gameOver = true;
        ToggleCanPlay();

        Time.timeScale = 0f;

        if (pauseButton != null)
            pauseButton.gameObject.SetActive(false);

        // 💀 Música de Game Over
        audioController.GameOverMusic();
        audioController.StopAllSFX();

        // 💾 Guarda el score actual cuando termina la partida
        ScoreManager.instance.SaveCurrentScore();
    }

    public void RestartButton()
    {
        // 🔁 Al darle restart, se detiene la música de GameOver y vuelve al menú
        audioController.ButtonSFX();
        audioController.MenuMusic();

        restartRequestTime = Time.unscaledTime;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    // 🆕 Mostrar panel informativo (puedes llamarlo desde un botón en el menú)
    public void ShowInfoPanel()
    {
        InfoPanel.SetActive(true);
        StartPanel.SetActive(false);
        Time.timeScale = 0f;
        canPlay = false;
    }

    // 🆕 Cerrar el panel informativo y volver al menú principal
    public void CloseInfoPanel()
    {
        InfoPanel.SetActive(false);
        StartPanel.SetActive(true);
    }
}
