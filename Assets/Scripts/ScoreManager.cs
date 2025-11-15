using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public AudioController audioController;

    [SerializeField] private TextMeshProUGUI _currentScoreText;
    [SerializeField] private TextMeshProUGUI _currentScoreTextFinal;
    [SerializeField] private TextMeshProUGUI _highScoreText;

    private int _score = 0;
    private int _highScore = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }



    private void Start()
    {
        // Cargar el HighScore guardado
        _highScore = PlayerPrefs.GetInt("HighScore", 0);

        // Mostrar el valor inicial
        _currentScoreText.text = _score.ToString();
        _currentScoreTextFinal.text = _score.ToString();
        _highScoreText.text = _highScore.ToString();
    }

    public void UpdateScore()
    {
        if (GameController.instance.canPlay)
        {
            _score++;
            _currentScoreText.text = _score.ToString();
            _currentScoreTextFinal.text = _score.ToString();

            // Si el nuevo score supera el highscore, se guarda
            if (_score > _highScore)
            {
                _highScore = _score;
                PlayerPrefs.SetInt("HighScore", _highScore);
                _highScoreText.text = _highScore.ToString();
            }

            // Sonido de punto
            audioController.PointSFX();

            // 🎵 Cambia la música al llegar a 10 puntos
            if (_score == 10)
            {
                audioController.GameThemeHardMusic();
            }
        }
    }



    public void SaveCurrentScore()
    {
        PlayerPrefs.SetInt("LastScore", _score);
        PlayerPrefs.Save();
    }

    public void LoadLastScore()
    {
        _score = PlayerPrefs.GetInt("LastScore", 0);
        _currentScoreText.text = _score.ToString();
        _currentScoreTextFinal.text = _score.ToString();
    }

    public void ResetScores()
    {
        PlayerPrefs.DeleteKey("HighScore");
        PlayerPrefs.DeleteKey("LastScore");
        _highScore = 0;
        _score = 0;
        _highScoreText.text = "0";
        _currentScoreText.text = "0";
        _currentScoreTextFinal.text = "0";
    }

    public int GetScore() => _score;
    public int GetHighScore() => _highScore;


}
