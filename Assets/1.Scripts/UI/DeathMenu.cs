using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{

    #region DeathUIAnim

    public static DeathMenu Instance;
    public GameObject[] DeathUI;
    public Vector2[] startPos;
    void Awake()
    {
        Instance = this;
        for (int i = 0; i < DeathUI.Length; i++)
        {
            startPos[i] = DeathUI[i].GetComponent<RectTransform>().localPosition;
        }
    }
    void ResetAll()
    {
        DeathUI[4].SetActive(false);
        DeathUI[5].SetActive(false);

        DeathUI[0].GetComponent<Image>().color = new Color(1, 1, 1, 0);
        DeathUI[4].GetComponent<Image>().color = new Color(1, 1, 1, 0);
        DeathUI[5].GetComponent<Image>().color = new Color(1, 1, 1, 0);

        DeathUI[0].GetComponent<RectTransform>().localPosition = startPos[0];
        DeathUI[1].GetComponent<RectTransform>().localPosition = new Vector2(startPos[1].x, -Screen.height + startPos[1].y);
        DeathUI[2].GetComponent<RectTransform>().localPosition = new Vector2(startPos[2].x, -Screen.height + startPos[2].y);
        DeathUI[3].GetComponent<RectTransform>().localPosition = new Vector2(startPos[3].x, -Screen.height + startPos[3].y);
        DeathUI[4].GetComponent<RectTransform>().localPosition = startPos[4];
        DeathUI[5].GetComponent<RectTransform>().localPosition = startPos[5];
    }
    public void StartDeathUIAnim()
    {
        ResetAll();
        TitleAnim();
        ScoreTableAnim();
    }
    void TitleAnim()
    {
        LeanTween.color(DeathUI[0].GetComponent<Image>().rectTransform, new Color(1, 1, 1, 1), 0.5f * 2f);
        LeanTween.moveLocalY(DeathUI[0], startPos[0].y + 200f, 0.2f).setDelay(0.2f).setEaseOutQuad();
        LeanTween.moveLocalY(DeathUI[0], startPos[0].y, 0.3f).setDelay(0.5f).setEaseOutBounce();
    }
    void ScoreTableAnim()
    {
        LeanTween.moveLocalY(DeathUI[1], startPos[1].y, 0.2f * 2f).setDelay(0.15f * 2f);
        LeanTween.moveLocalY(DeathUI[2], startPos[2].y, 0.2f * 2f).setDelay(0.15f * 2f);
        LeanTween.moveLocalY(DeathUI[3], startPos[3].y, 0.2f * 2f).setDelay(0.15f * 2f).setOnComplete(ButtonsAnim);
    }
    void ButtonsAnim()
    {
        DeathUI[4].SetActive(true);
        DeathUI[5].SetActive(true);
        LeanTween.color(DeathUI[4].GetComponent<Image>().rectTransform, new Color(1, 1, 1, 1), 0.2f * 2f).setDelay(0.2f);
        LeanTween.color(DeathUI[5].GetComponent<Image>().rectTransform, new Color(1, 1, 1, 1), 0.2f * 2f).setDelay(0.2f);
    }
    #endregion

    #region DeathUIScore

    public Text ScoreText, HighScoreText;
    public GameObject BronzeMedal, SilverMedal, GoldMedal, NewBest, ShineParticle;

    public void DeathUIScore()
    {
        StartCoroutine(SetMedal());
        StartCoroutine(IEScore());
        CheckNewBest();
    }
    IEnumerator IEScore()
    {
        HighScoreText.text = PlayerPrefs.GetInt("Highscore").ToString();
        ScoreText.text = "0";
        yield return new WaitForSeconds(0.85f);
        if (Score.Instance.score >= 10) LeanTween.color(BronzeMedal.GetComponent<Image>().rectTransform, new Color(1, 1, 1, 1), 0.7f);
        if (Score.Instance.score >= 30) LeanTween.color(SilverMedal.GetComponent<Image>().rectTransform, new Color(1, 1, 1, 1), 0.7f);
        if (Score.Instance.score >= 50) LeanTween.color(GoldMedal.GetComponent<Image>().rectTransform, new Color(1, 1, 1, 1), 0.7f);
        for (int i = 0; i <= Score.Instance.score; i++)
        {
            ScoreText.text = i.ToString();
            yield return new WaitForSeconds(0.06f);
        }
    }
    IEnumerator SetMedal()
    {
        ShineParticle.SetActive(false);
        BronzeMedal.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        SilverMedal.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        GoldMedal.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        if (Score.Instance.score >= 10)
        {
            BronzeMedal.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            ShineParticle.SetActive(true);
        }
        if (Score.Instance.score >= 30)
        {
            BronzeMedal.SetActive(false);
            SilverMedal.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            ShineParticle.SetActive(true);
        }
        if (Score.Instance.score >= 50)
        {
            BronzeMedal.SetActive(false);
            SilverMedal.SetActive(false);
            GoldMedal.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            ShineParticle.SetActive(true);
        }
        if (Score.Instance.score < 10)
        {
            BronzeMedal.SetActive(false);
            SilverMedal.SetActive(false);
            GoldMedal.SetActive(false);
        }
    }
    void CheckNewBest()
    {
        NewBest.SetActive(false);
        if (Score.Instance.newBest == true)
        {
            NewBest.SetActive(true);
        }
    }

    #endregion

}
