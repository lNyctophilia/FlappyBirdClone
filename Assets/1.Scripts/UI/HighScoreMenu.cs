using UnityEngine;
using UnityEngine.UI;

public class HighScoreMenu : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private RectTransform _scorePanel;
    [SerializeField] private GameObject _bronzeMedal, _silverMedal, _goldMedal, _shineParticle;
    [SerializeField] private Text _highScoreText;

    [Header("Animation Settings")]
    [SerializeField] private float _animationDuration = 0.4f;


    private float _closedYPosition;
    private bool _isMenuActive;
    private bool _isAnimating;


    private void Awake()
    {
        _closedYPosition = -Screen.height -140;
        GameManager.OnGameStateChanged += GameStateHandler;
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameStateHandler;
    }

    public void ToggleScoreMenu()
    {
        if (_isAnimating) return;
        
        _isAnimating = true;
        _isMenuActive = !_isMenuActive;

        if (_isMenuActive)
        {
            OpenMenu();
            UpdateMedals(PlayerPrefs.GetInt("Highscore"));
        }
        else
        {
            CloseMenu();
        }
    }

    private void GameStateHandler(GameState _currentState)
    {
        if(_currentState != GameState.Starting) return;

        CloseMenu();
        _isAnimating = false;
        _isMenuActive = false;
    }

    private void OpenMenu()
    {
        _scorePanel.gameObject.SetActive(true);
        _scorePanel.anchoredPosition = new Vector2(0, -Screen.height - 500);
        LeanTween.moveY(_scorePanel, -140, _animationDuration)
            .setOnComplete(() => _isAnimating = false);
    }

    private void CloseMenu()
    {
        

        LeanTween.moveY(_scorePanel, _closedYPosition, _animationDuration)
            .setOnComplete(() => {
                _scorePanel.gameObject.SetActive(false);
                _isAnimating = false;
            });
    }

    private void UpdateMedals(int highScore)
    {
        _highScoreText.text = highScore.ToString();
        
        _bronzeMedal.SetActive(highScore >= 10);
        _silverMedal.SetActive(highScore >= 30);
        _goldMedal.SetActive(highScore >= 50);
        
        _shineParticle.SetActive(highScore >= 10);
    }
}