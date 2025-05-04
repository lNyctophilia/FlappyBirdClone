using UnityEngine;
using UnityEngine.UI;


public class StartingMenu : MonoBehaviour
{
    public Image[] StartingMenuElements;
    public GameObject StartingButton;
    public void FadeStartingMenu()
    {
        StartingButton.SetActive(false);
        for (int i = 0; i < StartingMenuElements.Length; i++)
        {
            LeanTween.color(StartingMenuElements[i].rectTransform, new Color(1, 1, 1, 0), 0.4f).setOnComplete(FadeFinish);
        }
    }
    void FadeFinish()
    {
        UIManager.Instance.OpenGameMenu();
        for (int i = 0; i < StartingMenuElements.Length; i++)
        {
            StartingMenuElements[i].color = new Color(1, 1, 1, 1);
        }
        StartingButton.SetActive(true);
    }
}
