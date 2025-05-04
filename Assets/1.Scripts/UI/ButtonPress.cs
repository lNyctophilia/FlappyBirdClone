using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    Button button;
    float buttonHeight;

    void Start()
    {
        button = GetComponent<Button>();
        buttonHeight = button.GetComponent<RectTransform>().localPosition.y;
    }

    // IPointerDownHandler arayüz metodunu implement ediyoruz
    public void OnPointerDown(PointerEventData eventData) => Pressed();
    public void OnPointerUp(PointerEventData eventData) => UnPressed();

    void Pressed()
    {
        Audio.Instance.PlayVoice("Click");
        LeanTween.moveLocalY(button.gameObject, buttonHeight - 18f, 0.05f);
    }
    void UnPressed() => LeanTween.moveLocalY(button.gameObject, buttonHeight, 0.05f);
}

