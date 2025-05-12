using System;
using UnityEngine;

public class BirdScore : MonoBehaviour
{
    public static BirdScore Instance;
    public static event Action OnPlayerScored;
    private void Awake()
    {
        Instance = this;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("ScoreArea"))
        {
            OnPlayerScored?.Invoke();
        }
    }
}
