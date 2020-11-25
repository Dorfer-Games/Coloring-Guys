using DG.Tweening;
using Kuhpik;
using UnityEngine;

public class CellCollisionSystem : GameSystem, IIniting
{
    void IIniting.OnInit()
    {
        foreach (var character in game.characters)
        {
            character.rigidbody.GetComponent<OnCollisionEnterComponent>().OnEnter += ColorCell;
        }
    }

    void ColorCell(Transform other)
    {
        if (other.CompareTag("Cell"))
        {
            if (DOTween.IsTweening(other)) return;

            var renderer = other.GetComponent<MeshRenderer>();
            var seq = DOTween.Sequence();

            seq.Append(renderer.material.DOColor(Color.red, config.CellFadeTime).SetEase(Ease.OutCubic));
            seq.Append(other.DOMoveY(-20, config.CellFallTime).SetEase(Ease.Linear));
            seq.SetEase(Ease.Linear);
            seq.Play();
        }
    }
}
