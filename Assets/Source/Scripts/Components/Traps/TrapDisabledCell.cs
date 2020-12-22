using UnityEngine;
using DG.Tweening;
using System.Collections;

public class TrapDisabledCell : TrapsBehaviour
{
    [Header("Время через, которое ловушка активируется")]
    [SerializeField] private float timeDisabledTrap;

    [Header("Скорость падения ловушки")]
    [SerializeField] private float speed;

    [Header("Значение, которое управляет глубиной падения ловушки")]
    [SerializeField] private float levelTrap;


    private void Start()
    {
        StartCoroutine(timeStartTrap());
    }

    public override void MovementTrap()
    {
        float startYpos = transform.position.y;
        var seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMoveY((transform.position.y - levelTrap), speed));
        seq.AppendInterval(3f);
        seq.Append(transform.DOLocalMoveY(startYpos, speed));
        seq.Play();
        StartCoroutine(timeStartTrap());
    }

    private IEnumerator timeStartTrap()
    {
        yield return new WaitForSeconds(timeDisabledTrap);
        MovementTrap();
    }
}
