﻿using Kuhpik;
using UnityEngine;

public class InputControlSystem : GameSystem, IUpdating
{
    [SerializeField] float maxDelta;
    Vector2 touchPosition;

    void IUpdating.OnUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchPosition = Input.mousePosition;
        }

        else if (Input.GetMouseButton(0))
        {
            var mouse = Input.mousePosition;
            var delta = mouse.x - touchPosition.x;
            game.characters[0].rotationValue = Mathf.Clamp(delta / maxDelta, -1, 1);
        }

        else if (Input.GetMouseButtonUp(0))
        {
            game.characters[0].rotationValue = 0;
        }
    }
}