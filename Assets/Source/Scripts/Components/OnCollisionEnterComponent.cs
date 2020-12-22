using System;
using UnityEngine;


public class OnCollisionEnterComponent : MonoBehaviour
{
    public event Action<Transform, Transform> OnEnter;

    void OnCollisionEnter(Collision collision)
    {
        OnEnter?.Invoke(collision.transform, transform);
    }
}
