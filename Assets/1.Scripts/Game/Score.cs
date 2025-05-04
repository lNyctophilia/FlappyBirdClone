using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Score Instance;
    public int score, HighScore;
    public bool newBest;
    public Text ScoreText;
    private void Awake()
    {
        Instance = this;
        if (!PlayerPrefs.HasKey("Highscore"))
        {
            HighScore = 0;
            PlayerPrefs.SetInt("Highscore", HighScore);
        }
        else HighScore = PlayerPrefs.GetInt("Highscore");
    }
    public void ScoreReset()
    {
        score = 0;
        ScoreText.text = score.ToString();
        newBest = false;
    }
    public void IncreaseScore()
    {
        score++;
        ScoreText.text = score.ToString();
        Audio.Instance.PlayVoice("Score");
        if (score > PlayerPrefs.GetInt("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", score);
            newBest = true;
        }
    }
}
