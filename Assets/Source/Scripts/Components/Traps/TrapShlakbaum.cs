using UnityEngine;
using DG.Tweening;


public class TrapShlakbaum : TrapsBehaviour
{
    [SerializeField] private Transform trapMove;
    [Header("Move Trap")]
    [SerializeField] private float speedTraps;

    private void Start()
    {
        MovementTrap();
    }

    public override void MovementTrap()
    {
        trapMove.transform.DOLocalRotate(Vector3.forward * 360, speedTraps, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
    }


    private void OnTriggerEnter(Collider other)
    {
        TriggerTrapsSystem.triggerTrapsSystem.ImpulseTrap(other.transform);
    }
}
