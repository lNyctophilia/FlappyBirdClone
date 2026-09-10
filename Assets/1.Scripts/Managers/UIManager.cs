using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // TimeScale olayları ve Kuşu aktif etme olayları var unutma.

    [Header("System")]
    public static UIManager Instance;


    [Header("References")]
    [SerializeField] private GameObject[] _canvas;


    private void Awake()
    {
        Instance = this;
        GameManager.OnGameStateChanged += ChangeCanvas;
        InitCanvas();
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= ChangeCanvas;
    }

    private void InitCanvas()
    {
        if (_canvas == null || _canvas.Length == 0) return;

        for (int i = 0; i < _canvas.Length; i++)
        {
            if (_canvas[i] == null) continue;
            bool isMenu = (i == 0);
            _canvas[i].SetActive(isMenu);
            if (_canvas[i].TryGetComponent<CanvasGroup>(out var group))
            {
                group.alpha = isMenu ? 1f : 0f;
                group.blocksRaycasts = isMenu;
                group.interactable = isMenu;
            }
        }
    }


    private void ChangeCanvas(GameState _currentState)
    {
        StartCoroutine(OpenCanvas((int)_currentState));
    }
    private IEnumerator OpenCanvas(int _canvasIndex)
    {
        if(_canvasIndex <= 1)
            yield return new WaitForSeconds(Transition.Instance.FadeDuration);

        for(int i = 0; i < _canvas.Length; i++)
        {
            if(i != _canvasIndex && _canvas[i] != null)
            {
                int currentIndex = i;
                if(_canvas[currentIndex].TryGetComponent<CanvasGroup>(out var closingGroup))
                {
                    closingGroup.blocksRaycasts = false;
                    closingGroup.interactable = false;
                }

                if(i > 1)
                {
                    LeanTween.alphaCanvas(_canvas[currentIndex].GetComponent<CanvasGroup>(), 0, Transition.Instance.FadeDuration / 2f).setOnComplete(() => { 
                        _canvas[currentIndex].SetActive(false); 
                    }).setIgnoreTimeScale(true);
                }
                else if(i == 1)
                {
                    LeanTween.alphaCanvas(_canvas[currentIndex].GetComponent<CanvasGroup>(), 0, Transition.Instance.FadeDuration * 1.4f).setOnComplete(() => { 
                        _canvas[currentIndex].SetActive(false); 
                    }).setIgnoreTimeScale(true);
                }
                else
                {
                    _canvas[i].SetActive(false); 
                }
            }
        }

        LeanTween.cancel(_canvas[_canvasIndex]);
        _canvas[_canvasIndex].gameObject.SetActive(true);

        if(_canvas[_canvasIndex].TryGetComponent<CanvasGroup>(out var openingGroup))
        {
            openingGroup.alpha = 0;
            openingGroup.blocksRaycasts = true;
            openingGroup.interactable = true;
            LeanTween.alphaCanvas(openingGroup, 1, Transition.Instance.FadeDuration).setIgnoreTimeScale(true);
        }
    }
}