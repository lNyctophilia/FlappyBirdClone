using UnityEngine;
using UnityEngine.Events;

public class PauseManager : MonoBehaviour
{
    private void Awake() => GameManager.OnGameStateChanged += TogglePause;
    private void OnDestroy() => GameManager.OnGameStateChanged -= TogglePause;

    public void TogglePause(GameState _currentState)
    {
        switch(_currentState)
        {
            case GameState.Playing: Time.timeScale = 1; break;
            case GameState.Pause: Time.timeScale = 0; break;
        }
    }
}