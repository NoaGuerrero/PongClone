using UnityEngine;
using TMPro; 

public class HighScoreDisplay : MonoBehaviour
{
    [Header("Texto donde se mostrará el High Score (en el menú)")]
    [SerializeField] private TMP_Text highScoreText;

    private const string HIGH_SCORE_KEY = "HighScore";

    private void Start()
    {
        int highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore;
        }
    }
}
