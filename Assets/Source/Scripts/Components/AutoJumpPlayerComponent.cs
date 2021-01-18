﻿using Kuhpik;
using Supyrb;
using UnityEngine;

public class AutoJumpPlayerComponent : GameSystem
{
    [Header("Settings Raycast")]
    [SerializeField] private float offset; // смещение луча
    [SerializeField] private float distance; // дистанция луча
    [SerializeField] private float upPosition; // весота луча

    private bool thisPlayer; // если true, то значит это игрок, которым мы управляем

    public bool Jump; // true, если наш игрок прыгнул


    private void Start()
    {
        if (transform.name == "Player")
        {
            thisPlayer = true;
        }
    }

    private void Update()
    {
        if (thisPlayer)
        {
#if UNITY_EDITOR
            Debug.DrawRay(transform.position + (transform.up * upPosition) + (transform.forward * offset), -Vector3.up * distance, Color.black);
#endif
            Ray ray = new Ray(transform.position + (transform.up * upPosition) + (transform.forward * offset), -Vector3.up * distance);
            if (Physics.Raycast(ray, out var hit, distance))
            {
                var cell = hit.collider.GetComponentInParent<CellComponent>();
                if (!Jump && cell.Cell.transform.position.y < -7)
                {
                    if (cell.IsDown == true) {
                        Signals.Get<JumpReadySignal>().Dispatch(0);
                        Jump = true;
                    }
                    }
                }
            }
        }
    }