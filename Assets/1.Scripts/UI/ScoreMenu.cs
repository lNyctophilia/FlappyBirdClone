using UnityEngine;
using UnityEngine.UI;

public class ScoreMenu : MonoBehaviour
{
    public static ScoreMenu Instance;
    public GameObject ScoreMenuUI, BronzeMedal, SilverMedal, GoldMedal, ShineParticle;
    public Text HighScore;
    public bool isScoreMenuActive, isTransition;
    void Awake()
    {
        Instance = this;
    }
    public void SetScoreMenu()
    {
        if (!isScoreMenuActive && !isTransition)
        {
            isScoreMenuActive = true;
            isTransition = true;
            ScoreMenuUI.SetActive(true);
            ScoreMenuUI.GetComponent<RectTransform>().localPosition = new Vector3(0, -Screen.height -500, 0);
            LeanTween.moveLocalY(ScoreMenuUI, -140, 0.4f).setOnComplete(StopTransition);
            Audio.Instance.PlayVoice("ScoreMenu");
        }
        else if (isScoreMenuActive && !isTransition)
        {
            isTransition = true;
            ScoreMenuUI.GetComponent<RectTransform>().localPosition = new Vector3(0, -140, 0);
            LeanTween.moveLocalY(ScoreMenuUI, -2500, 0.4f).setOnComplete(closeScMn);
            Audio.Instance.PlayVoice("ScoreMenu");
        }

        HighScore.text = PlayerPrefs.GetInt("Highscore").ToString();

        if (PlayerPrefs.GetInt("Highscore") >= 10)
        {
            BronzeMedal.SetActive(true);
            ShineParticle.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Highscore") >= 30)
        {
            BronzeMedal.SetActive(false);
            SilverMedal.SetActive(true);
            ShineParticle.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Highscore") >= 50)
        {
            BronzeMedal.SetActive(false);
            SilverMedal.SetActive(false);
            GoldMedal.SetActive(true);
            ShineParticle.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Highscore") < 10)
        {
            ShineParticle.SetActive(false);
            BronzeMedal.SetActive(false);
            SilverMedal.SetActive(false);
            GoldMedal.SetActive(false);
        }
    }
    void closeScMn()
    {
        ScoreMenuUI.SetActive(false);
        isScoreMenuActive = false;
        StopTransition();
    }
    void StopTransition() => isTransition = false;
}
