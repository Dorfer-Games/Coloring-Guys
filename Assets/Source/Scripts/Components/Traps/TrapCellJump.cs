using UnityEngine;
using DG.Tweening;

public class TrapCellJump : TrapsBehaviour
{

    [Header("Move Trap")]
    [SerializeField] private float forceUpTrap;

    [Header("Значение, которое управляет глубиной поднятия ловушки")]
    [SerializeField] private float levelTrap;

    public override void MovementTrap()
    {
        float startYpos = transform.position.y;
        var seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMoveY((transform.position.y + levelTrap), forceUpTrap));
        seq.AppendInterval(1f);
        seq.Append(transform.DOLocalMoveY(startYpos, forceUpTrap));
        seq.Play();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        MovementTrap();
        TriggerTrapsSystem.triggerTrapsSystem.JumpTrap(other.transform);
    }
}
