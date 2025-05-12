using UnityEngine;

public class BirdFreeze : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _anim;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();

        GameManager.OnGameStateChanged += HandleFreeze;
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= HandleFreeze;
    }
    private void HandleFreeze(GameState _currentState)
{
    switch(_currentState)
    {
        case GameState.Starting:
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;
            _anim.speed = 1;
            break;
        case GameState.Ending:
            _anim.speed = 0;
            break;
        case GameState.Playing:
            _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            _anim.speed = 1;
            break;
    }
}
}
