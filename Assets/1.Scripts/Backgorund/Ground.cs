using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    Vector2 StartPos;
    float repeatWidth;
    float yon = -1f;
    [SerializeField, Range(0, 10)] float Speed;
    [SerializeField, Range(0, 10)] float katSayi = 1f;

    private void Start()
    {
        StartPos = transform.position;
        repeatWidth = gameObject.GetComponent<BoxCollider2D>().size.x / 2;
    }
    private void FixedUpdate()
    {
        if (!BirdDeath.Instance.isDeath)
            transform.Translate(new Vector3(yon * Speed * Time.deltaTime, 0, 0), Space.World);

        if (transform.position.x < StartPos.x - repeatWidth / katSayi)
            transform.position = StartPos;

    }
}
