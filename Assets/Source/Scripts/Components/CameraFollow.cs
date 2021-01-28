using Kuhpik;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameConfig config;
    [SerializeField] private Vector3 offset, velocity;
    [SerializeField] private Transform follower;
    [SerializeField] private Transform target;
    [SerializeField] private float translateSpeed;
    [SerializeField] private float rotationSpeed;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void FixedUpdate()
    {
        HandleTranslation();
        offset = new Vector3(0, config.GetValue(EGameValue.CameraXPos), config.GetValue(EGameValue.CameraYPos));
        transform.eulerAngles = new Vector3(config.GetValue(EGameValue.CameraRotation),0,0);
    }

    private void HandleTranslation()
    {
        var targetPosition = target.transform.position + offset;
        follower.position = Vector3.SmoothDamp(follower.position, targetPosition, ref velocity, translateSpeed);
    }
}

