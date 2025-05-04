using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField, Range(0, 10)] float Speed;
    float yon = -1f;
    private void FixedUpdate()
    {
        if (!BirdDeath.Instance.isDeath)
            transform.Translate(new Vector3(yon * Speed * Time.deltaTime, 0, 0), Space.World);
    }
}
