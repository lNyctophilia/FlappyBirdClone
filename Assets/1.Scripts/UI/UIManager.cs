using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject Main, Starting, Game, Pause, Death;
    public GameObject Bird;
    private void Awake()
    {
        Instance = this;
        OpenMainMenu();
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        QualitySettings.SetQualityLevel(1);
    }
    public void OpenMainMenu()
    {
        Time.timeScale = 1;
        Bird.SetActive(false);
        Main.SetActive(true);
        Starting.SetActive(false);
        Game.SetActive(false);
        Pause.SetActive(false);
        Death.SetActive(false);
        ScoreMenu.Instance.ScoreMenuUI.SetActive(false);
        ScoreMenu.Instance.isScoreMenuActive = false;
    }
    public void OpenStartingMenu()
    {
        Time.timeScale = 1;
        Score.Instance.ScoreReset();
        Bird.SetActive(true);
        Main.SetActive(false);
        Starting.SetActive(true);
        Game.SetActive(true);
        Pause.SetActive(false);
        Death.SetActive(false);
    }
    public void OpenGameMenu()
    {
        Time.timeScale = 1;
        Main.SetActive(false);
        Starting.SetActive(false);
        Game.SetActive(true);
        Pause.SetActive(false);
        Death.SetActive(false);
    }
    public void OpenPauseMenu()
    {
        Time.timeScale = 0;
        Main.SetActive(false);
        Starting.SetActive(false);
        Game.SetActive(true);
        Pause.SetActive(true);
        Death.SetActive(false);
    }
    public void OpenDeathMenu()
    {
        Time.timeScale = 1;
        Main.SetActive(false);
        Starting.SetActive(false);
        Game.SetActive(false);
        Pause.SetActive(false);
        Death.SetActive(true);
    }
    public void CloseGameMenu()
    {
        Time.timeScale = 1;
        Game.SetActive(false);
    }
}