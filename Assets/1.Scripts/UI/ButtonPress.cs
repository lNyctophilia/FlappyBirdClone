using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ButtonPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("References")]
    private Button button;


    [Header("Settings")]
    [SerializeField] private float _buttonPressedHeightDistance = 18f;
    [SerializeField] private float _duration = 0.05f;
    [SerializeField] private bool isMuted;
    private float _buttonHeight;


    // ----- SYSTEM -----
    public static event Action OnButtonPressed;


    private void Start()
    {
        button = GetComponent<Button>();
        _buttonHeight = button.GetComponent<RectTransform>().localPosition.y;
    }


    public void OnPointerDown(PointerEventData eventData) => Pressed();
    public void OnPointerUp(PointerEventData eventData) => UnPressed();

    private void Pressed()
    {
        LeanTween.moveLocalY(button.gameObject, _buttonHeight - _buttonPressedHeightDistance, _duration);
        if(!isMuted) OnButtonPressed?.Invoke();
    }
    private void UnPressed() => LeanTween.moveLocalY(button.gameObject, _buttonHeight, _duration);
}

