using UnityEngine;
using TMPro; 

public class ScoreManager : MonoBehaviour
{
    // ✅ Singleton para acceder al marcador desde cualquier script
    public static ScoreManager Instance { get; private set; }

    [Header("Referencias UI (en la escena del juego)")]
    [SerializeField] private TMP_Text scoreText;       // Texto que muestra el puntaje actual
    [SerializeField] private TMP_Text highScoreText;   // Texto que muestra el puntaje más alto

    private int currentScore = 0;
    private int highScore = 0;

    private const string HIGH_SCORE_KEY = "HighScore";

    private void Awake()
    {
        // Garantizar que solo exista un ScoreManager en la escena
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // Mantener entre escenas (Menu → Juego)

        // Cargar el puntaje más alto guardado (si no existe, será 0)
        highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
        UpdateScoreUI();
        UpdateHighScoreUI();
    }

    /// <summary>
    /// Añade puntos al marcador actual.
    /// </summary>
    public void AddPoint(int points = 1)
    {
        currentScore += points;
        UpdateScoreUI();

        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, highScore);
            PlayerPrefs.Save();
            UpdateHighScoreUI();
        }
    }

    /// <summary>
    /// Reinicia el marcador actual (sin borrar el High Score).
    /// </summary>
    public void ResetScore()
    {
        currentScore = 0;
        UpdateScoreUI();
    }

    /// <summary>
    /// Borra el High Score (solo para pruebas).
    /// </summary>
    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey(HIGH_SCORE_KEY);
        PlayerPrefs.Save();
        highScore = 0;
        UpdateHighScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + currentScore;
    }

    private void UpdateHighScoreUI()
    {
        if (highScoreText != null)
            highScoreText.text = "High Score: " + highScore;
    }

    // Métodos para asignar textos en caso de que los crees después
    public void SetScoreText(TMP_Text txt)
    {
        scoreText = txt;
        UpdateScoreUI();
    }

    public void SetHighScoreText(TMP_Text txt)
    {
        highScoreText = txt;
        UpdateHighScoreUI();
    }
}
