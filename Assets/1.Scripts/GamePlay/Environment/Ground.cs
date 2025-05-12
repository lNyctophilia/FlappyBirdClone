using UnityEngine;

public class Ground : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer _groundRenderer;

    [Header("Movement Settings")]
    [SerializeField, Range(0, 10)] private float _speed = 2f;


    private Vector2 _initialPosition;
    private float _halfSpriteWidth;
    private const float MOVEMENT_DIRECTION = -1f; // Sabit değerler büyük harfle

    private void Awake()
    {
        // Sprite genişliğini doğru şekilde al
        _halfSpriteWidth = _groundRenderer.sprite.bounds.size.x / 2;
        _initialPosition = transform.position;
    }

    private void FixedUpdate()
    {
        // Oyun durumu kontrolü (Encapsulation'a uygun hale getirildi)
        if (!GameManager.Instance.ReadyToMove) return;

        MoveGround();
        CheckResetThreshold();
    }

    private void MoveGround()
    {
        float movement = MOVEMENT_DIRECTION * _speed * Time.deltaTime;
        transform.Translate(new Vector3(movement, 0, 0), Space.World);
    }

    private void CheckResetThreshold()
    {
        float resetThreshold = _initialPosition.x - _halfSpriteWidth;
        if (transform.position.x <= resetThreshold)
        {
            transform.position = _initialPosition;
        }
    }
}