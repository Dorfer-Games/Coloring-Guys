﻿using System;
using UnityEngine;

public class OnTriggerEnterComponent : MonoBehaviour
{
    public event Action<Transform, Transform> OnEnter;

    void OnTriggerEnter(Collider other)
    {
        OnEnter?.Invoke(other.transform, transform);
    }
}
