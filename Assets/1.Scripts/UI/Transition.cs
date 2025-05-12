using System;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image _blackImage;
    [SerializeField] private Image _flashImage;
    

    [Header("Settings")]
    [SerializeField] private float _fadeDuration = 0.3f;
    [SerializeField] private float _flashDuration = 0.1f;
    public float FadeDuration => _fadeDuration;


    [Header("System")]
    public static Transition Instance;
    public static event Action OnFadeTransition;
    public static event Action OnFadeTransitionFinished;


    private void Awake()
    {
        Instance = this;
        _blackImage.gameObject.SetActive(false);
        _flashImage.gameObject.SetActive(false);

        GameManager.OnGameStateChanged += Fade;
        GameManager.OnGameStateChanged += Flash;
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= Fade;
        GameManager.OnGameStateChanged -= Flash;
    }


    public void Fade(GameState _currentState)
    {
        if (_currentState == GameState.Pause || _currentState == GameState.Ending || _currentState == GameState.Playing) return;

        _blackImage.gameObject.SetActive(true);
        _blackImage.color = new Color(0, 0, 0, 0);
        LeanTween.cancel(_blackImage.gameObject);
        LeanTween.alpha(_blackImage.rectTransform, 1, _fadeDuration).setOnComplete(() => {
            LeanTween.alpha(_blackImage.rectTransform, 0, _fadeDuration).setOnComplete(() => { _blackImage.gameObject.SetActive(false); }).setDelay(0.1f);
            OnFadeTransitionFinished?.Invoke();
        });

        OnFadeTransition?.Invoke();
    }
    public void Flash(GameState _currentState)
    {
        if(_currentState != GameState.Ending) return;

        _flashImage.gameObject.SetActive(true);
        _flashImage.color = new Color(1, 1, 1, 0);
        LeanTween.cancel(_flashImage.gameObject);
        LeanTween.alpha(_flashImage.rectTransform, 1, _flashDuration).setOnComplete(() => {
            LeanTween.alpha(_flashImage.rectTransform, 0, _flashDuration * 3).setOnComplete(() => { _flashImage.gameObject.SetActive(false); });
        });
    }


    #region ----- TESTING -----
/*
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Fade();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Flash();
        }
    }
*/
    #endregion
}
