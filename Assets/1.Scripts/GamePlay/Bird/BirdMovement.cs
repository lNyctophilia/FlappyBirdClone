using System;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D _rb;


    [Header("Settings")]
    [SerializeField] private float _jumpForce = 1.6f;


    [Header("System")]
    public static BirdMovement Instance;
    public static event Action OnPlayerJumping;


    private void Awake()
    {
        Instance = this;
        _rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (_rb.velocity.y > 0)
        {
            LeanTween.cancel(gameObject);
            LeanTween.rotateZ(gameObject, 35, 0.1f);
        }
        else if (_rb.velocity.y < 0)
        {
            LeanTween.cancel(gameObject);
            LeanTween.rotateZ(gameObject, -35, 0.2f);
        }        
    }
    public void Jump()
    {
        if(Time.timeScale == 1)
        {
            _rb.velocity = Vector2.up * _jumpForce;
            OnPlayerJumping?.Invoke();
        }
    }
}
