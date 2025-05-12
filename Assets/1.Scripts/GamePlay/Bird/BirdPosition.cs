using UnityEngine;

public class BirdPosition : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Vector3 _initialPos;


    private void Awake()
    {
        _initialPos = transform.position;

        GameManager.OnGameStateChanged += ResetPosition;
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= ResetPosition;
    }
    private void ResetPosition(GameState _currentState)
    {
        if(_currentState != GameState.Starting) return;

        transform.position = _initialPos;
        transform.rotation = Quaternion.identity;
    }
}
