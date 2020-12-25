using UnityEngine;
using DG.Tweening;
using System.Collections;
using NaughtyAttributes;

public class TrapDisabledCell : TrapsBehaviour
{
    [SerializeField] [Tag] private string tagObject;
    [SerializeField] private Color colorDistanceTrap;

    [Header("Время через, которое ловушка активируется")]
    [SerializeField] private float timeDisabledTrap;

    [Header("Скорость падения ловушки")]
    [SerializeField] private float speed;

    [Header("Значение, которое управляет глубиной падения ловушки")]
    [SerializeField] private float levelTrap;




    public override void MovementTrap()
    {
        float startYpos = transform.position.y;
        var seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMoveY((transform.position.y - levelTrap), speed));
        seq.AppendInterval(1f);
        seq.Append(transform.DOLocalMoveY(startYpos, speed));
        seq.Play();
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagObject))
        {
            UpdateColor(colorDistanceTrap);
            StartCoroutine(timeStartTrap());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tagObject))
        {
            UpdateColor(Color.white);
        }
    }

    private IEnumerator timeStartTrap()
    {
        yield return new WaitForSeconds(timeDisabledTrap);
        MovementTrap();
    }
}
