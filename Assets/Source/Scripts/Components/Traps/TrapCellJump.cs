using UnityEngine;
using DG.Tweening;

public class TrapCellJump : TrapsBehaviour
{

    [Header("Move Trap")]
    [SerializeField] private float forceUpTrap;

    [Header("Значение, которое управляет глубиной ловушки")]
    [SerializeField] private float levelTrapTop;
    [SerializeField] private float levelTrapBottom;

    public override void MovementTrap()
    {
        var seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMoveY(levelTrapTop, forceUpTrap));
        seq.AppendInterval(1f);
        seq.Append(transform.DOLocalMoveY(levelTrapBottom, forceUpTrap));
        seq.Play();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        MovementTrap();
        TriggerTrapsSystem.triggerTrapsSystem.JumpTrap(other.transform);
    }
}
