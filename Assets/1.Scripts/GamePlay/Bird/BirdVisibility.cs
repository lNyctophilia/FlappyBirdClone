using System.Collections;
using UnityEngine;

public class BirdVisibility : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _bird;

    private void Awake()
    {
        _bird.SetActive(false);
        GameManager.OnGameStateChanged += SetVisibility;
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= SetVisibility;
    }
    private void SetVisibility(GameState _currentState)
    {
        bool isVisible = _currentState != GameState.Menu;
        StartCoroutine(ChangeVisibility(isVisible));
    }

    private IEnumerator ChangeVisibility(bool isVisible)
    {
        yield return new WaitForSeconds(Transition.Instance.FadeDuration);
        _bird.SetActive(isVisible);
    }
}
