using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EndingAnimations : MonoBehaviour
{
    [Header("Button's Reference")]
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _shareButton;

    [Header("Button's Settings")]
    [SerializeField] private float _buttonsFadeDuration = 0.4f;


    [Space(10)]


    [Header("Score Panel's Reference")]
    [SerializeField] private GameObject _scorePanel;
    [SerializeField] private GameObject _currentScore;
    [SerializeField] private GameObject _highScore;

    [Header("Score Panel's Settings")]
    [SerializeField] private float _scorePanelSwipeDuration = 0.4f;


    [Space(10)]


    [Header("Game Over Title Reference")]
    [SerializeField] private GameObject _gameOverTitle;

    [Header("Game Over Title Settings")]
    [SerializeField] private float _gameOverTitleDuration = 0.4f;


    [Space(10)]


    [Header("Medal's References")]
    [SerializeField] private Image _bronzeMedal;
    [SerializeField] private Image _silverMedal;
    [SerializeField] private Image _goldMedal;
    [SerializeField] private GameObject _shineEffect;
    [SerializeField] private GameObject _newBestHighScore;
    private bool _newBestHighScored;

    [Header("Medal's Settings")]
    [SerializeField] private float _medalDuration = 0.6f;


    [Space(10)]


    [Header("Color Variables")]
    private Color _transparentColor = new Color(1, 1, 1, 0);
    private bool _isSharing = false;
    


    private void Awake()
    {
        GameManager.OnGameStateChanged += GameStateHandler;
        ScoreManager.OnNewBestHighScored += NewBestScore;

        if (_shareButton != null)
            _shareButton.onClick.AddListener(ShareScore);
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameStateHandler;
        ScoreManager.OnNewBestHighScored -= NewBestScore;

        if (_shareButton != null)
            _shareButton.onClick.RemoveListener(ShareScore);
    }


    private void GameStateHandler(GameState _currentState)
    {
        if(_currentState == GameState.Ending)
        {
            ResetUI();
            StartCoroutine(PlayEndingSequence());
        }
        else _newBestHighScored = false;
    }


    private IEnumerator PlayEndingSequence()
    {   
        AnimateGameOverTitle();
        yield return new WaitForSeconds(_gameOverTitleDuration / 2);
        AnimateScorePanel();
        yield return new WaitForSeconds(_scorePanelSwipeDuration);
        AnimateButtons();
        yield return new WaitForSeconds(_scorePanelSwipeDuration / 2);
        UpdateScores();
    }
    private void ResetUI()
    {
        // ----- Buttons -----
        _homeButton.interactable = false;
        _shareButton.interactable = false;
        _homeButton.GetComponent<Image>().color = _transparentColor;
        _shareButton.GetComponent<Image>().color = _transparentColor;


        // ----- Score Panel's -----
        _scorePanel.transform.localPosition = new Vector3(0, -Screen.height);
        _currentScore.transform.localPosition = new Vector3(350, -Screen.height + 60); // Magic Number
        _highScore.transform.localPosition = new Vector3(350, -Screen.height - 145); // Magic Number
        _currentScore.GetComponent<Text>().text = "0";
        _highScore.GetComponent<Text>().text = "0";


        // ----- GameOver Title -----
        _gameOverTitle.transform.localPosition = new Vector3(0, -Screen.height + 550); // Magic Number


        // ----- Medal's -----
        _bronzeMedal.color = _transparentColor;
        _silverMedal.color = _transparentColor;
        _goldMedal.color = _transparentColor;
        _shineEffect.SetActive(false);
        _newBestHighScore.SetActive(_newBestHighScored);
    }


    private void AnimateButtons()
    {
        LeanTween.color(_homeButton.GetComponent<Image>().rectTransform, Color.white, _buttonsFadeDuration);
        LeanTween.color(_shareButton.GetComponent<Image>().rectTransform, Color.white, _buttonsFadeDuration)
        .setOnComplete(() => 
        {
            _homeButton.interactable = true;
            _shareButton.interactable = true; 
        });
    }
    private void AnimateScorePanel()
    {
        LeanTween.moveLocalY(_scorePanel, 0, _scorePanelSwipeDuration); // Magic Number
        LeanTween.moveLocalY(_currentScore, 60, _scorePanelSwipeDuration); // Magic Number
        LeanTween.moveLocalY(_highScore, -145, _scorePanelSwipeDuration); // Magic Number
    }
    private void AnimateGameOverTitle()
    {
        LeanTween.moveLocalY(_gameOverTitle, 750, _gameOverTitleDuration) // Magic Number
        .setOnComplete(() => 
        {
            LeanTween.moveLocalY(_gameOverTitle, 500, _gameOverTitleDuration).setEaseOutBounce(); // Magic Number
        }).setEaseOutQuad();
    }
    private void UpdateScores()
    {
        int currentScore = ScoreManager.Instance.CurrentScore;
        int highScore = PlayerPrefs.GetInt("Highscore");
        StartCoroutine(AnimateCurrentScore(currentScore));
        StartCoroutine(AnimateHighScore(highScore));
        SetMedal(currentScore);
        NewBestScore();
    }
    private IEnumerator AnimateCurrentScore(int currentScore)
    {
        for (int i = 0; i <= currentScore; i++)
        {
            _currentScore.GetComponent<Text>().text = i.ToString();
            yield return new WaitForSeconds(AnimateScoreDuration(currentScore));
        }
    }
    private IEnumerator AnimateHighScore(int highScore)
    {
        for (int i = 0; i <= highScore; i++)
        {
            _highScore.GetComponent<Text>().text = i.ToString();
            yield return new WaitForSeconds(AnimateScoreDuration(highScore));
        }
    }
    private void SetMedal(int _currentScore)
    {
        if (_currentScore >= 50) ShowMedal(_goldMedal);
        else if (_currentScore >= 30) ShowMedal(_silverMedal);
        else if (_currentScore >= 10) ShowMedal(_bronzeMedal);
    }
    private void ShowMedal(Image _medal)
    {
        LeanTween.color(_medal.rectTransform, Color.white, _medalDuration).setDelay(_scorePanelSwipeDuration);;
        _shineEffect.SetActive(true);
    }
    private void NewBestScore() => _newBestHighScored = true;
    private float AnimateScoreDuration(int score)
    {
        float _totalAnimDuration = 0.6f;
        return _totalAnimDuration / Mathf.Max(score, 1);
    }

    public void ShareScore()
    {
        if (!_isSharing)
            StartCoroutine(TakeScreenshotAndShare());
    }

    private IEnumerator TakeScreenshotAndShare()
    {
        _isSharing = true;
        yield return new WaitForEndOfFrame();

        Texture2D screenTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenTexture.Apply();

        int score = ScoreManager.Instance != null ? ScoreManager.Instance.CurrentScore : 0;

        new NativeShare()
            .AddFile(screenTexture, "FlappyScore.png")
            .SetSubject("Flappy Bird Score")
            .SetText($"Flappy Bird'de {score} skor yaptım! Beni geçebilir misin?")
            .SetTitle("Skorunu Paylaş")
            .SetCallback((result, shareTarget) => _isSharing = false)
            .Share();

        Destroy(screenTexture);

        yield return new WaitForSecondsRealtime(1f);
        _isSharing = false;
    }
}
