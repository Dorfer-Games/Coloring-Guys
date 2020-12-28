using NaughtyAttributes;
using System;
using Kuhpik;
using UnityEngine;

public class TriggerTrapsSystem : GameSystem, ImpulseTrap, JumpTrap
{
    public static TriggerTrapsSystem triggerTrapsSystem { get; private set; }
    [SerializeField] [Tag] private string tagObjectCollision;
    private TrapsBehaviour[] trapsBehaviour;
    


    private void Start()
    {
        if (triggerTrapsSystem == null)
        {
            triggerTrapsSystem = this;
        }
    }

    public void ImpulseTrap(Transform other)
    {
        if (other.CompareTag(tagObjectCollision))
        {
            var normilized = other.transform.position.normalized;
            other.GetComponent<Rigidbody>().AddForce((-normilized + Vector3.up) * config.GetValue(EGameValue.ForceTrapImpulse), ForceMode.Impulse);
        }
    }

    public void JumpTrap(Transform other)
    {
        if (other.CompareTag(tagObjectCollision))
        {
            other.GetComponent<Rigidbody>().AddForce(Vector3.up * config.GetValue(EGameValue.HitImpulse), ForceMode.Impulse);
        }
    }
}
