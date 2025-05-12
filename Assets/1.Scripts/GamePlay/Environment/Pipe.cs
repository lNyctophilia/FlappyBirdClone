using UnityEngine;

public class Pipe : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField, Range(0, 10)] private float _speed = 0.6f;
    private const float _directionX = -1f;

    private void FixedUpdate()
    {
        if (!GameManager.Instance.ReadyToMove) return;

        float _movement = _directionX * _speed * Time.deltaTime;
        transform.Translate(new Vector3(_movement, 0, 0), Space.World);
    }
}