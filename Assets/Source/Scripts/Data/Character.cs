﻿using UnityEngine;

public class Character
{
    public Animator animator;
    public float rotationValue;
    public Rigidbody rigidbody;
    public Color color; //Цвет игрока
    public int stacks; //Сколько клеток мы можем покрасить
    public bool isJumping;

    public OnCollisionEnterComponent onCollisionComponent;
    public OnTriggerEnterComponent onTriggerComponent;
}
