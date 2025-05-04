using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    public static BirdMovement Instance;
    [SerializeField] float JumpForce = 100f;
    Rigidbody2D rb;
    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (rb.velocity.y > 0)
        {
            LeanTween.cancel(gameObject);
            LeanTween.rotateZ(gameObject, 35, 0.1f);
        }
        else if (rb.velocity.y < 0)
        {
            LeanTween.cancel(gameObject);
            LeanTween.rotateZ(gameObject, -35, 0.2f);
        }        
    }
    public void Freeze() => rb.constraints = RigidbodyConstraints2D.FreezeAll;
    public void UnFreeze()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    public void Jump()
    {
        if(!BirdDeath.Instance.isDeath && Time.timeScale == 1)
        {
            Audio.Instance.PlayVoice("Jump");
            rb.velocity = Vector2.up * JumpForce;
        }
    }
}
