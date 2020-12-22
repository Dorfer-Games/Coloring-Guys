using UnityEngine;
using System;


public interface ImpulseTrap
{
    void ImpulseTrap(Transform other);
}

public interface JumpTrap
{
    void JumpTrap(Transform other);
}

public class TrapsBehaviour : MonoBehaviour
{
    public virtual void MovementTrap()
    {

    }
}