using UnityEngine;
using System.Collections.Generic;

public class Character
{
    public Animator animator;
    public float rotationValue;
    public Rigidbody rigidbody;
    public Color color; //Цвет игрока
    public int stacks; //Сколько клеток мы можем покрасить
    public int colored; //Сколько клеток закрасил
    public bool isJumping;
    public bool isDeath;
    public bool isPlayer;
    public bool canIncreaseCells = true;
    public int levelOfMistake;
    public List<CellComponent> increasedCells;

    public OnTriggerEnterImpactComponent onTriggerEnterImpact;
    public OnTriggerEnterComponent onTriggerComponent;
    public OnCollisionEnterComponent onCollisionComponent;
    public AudioComponent audioComponent;
    public AutoJumpPlayerComponent jumpPlayerComponent;
}
