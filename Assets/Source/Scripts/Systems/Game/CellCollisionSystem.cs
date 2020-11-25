using DG.Tweening;
using Kuhpik;
using System.Linq;
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

    void ColorCell(Transform other, Transform moving)
    {
        if (other.CompareTag("Cell"))
        {
            if (DOTween.IsTweening(other)) return;

            var character = game.characters.First(x => x.rigidbody.transform == moving);
            var component = other.parent.GetComponent<CellComponent>();

            if (character.stacks > 0) ColorCell(character, component);
            else if (character.color != component.Color) FadeCell(other, component);            
        }
    }

    private static void ColorCell(Character character, CellComponent component)
    {
        component.Renderer.material.color = character.color;
        component.SetColor(character.color);
        character.stacks--;
    }

    private void FadeCell(Transform other, CellComponent component)
    {
        var renderer = component.Renderer;
        var seq = DOTween.Sequence();

        seq.Append(renderer.material.DOColor(Color.red, config.CellFadeTime).SetEase(Ease.OutCubic));
        seq.Append(other.DOMoveY(-20, config.CellFallTime).SetEase(Ease.Linear));
        seq.OnComplete(() => BringCellBack(other));
        seq.SetEase(Ease.Linear);
        seq.Play();
    }

    void BringCellBack(Transform cell)
    {
        var component = cell.parent.GetComponent<CellComponent>();
        component.Renderer.material.color = Color.white;
        component.SetColor(Color.white);
        component.SetDown(true);

        cell.DOMoveY(0, config.CellFallTime).SetDelay(config.CellBackTime).SetEase(Ease.Linear).OnComplete(() =>
        {
            component.SetDown(false);
        });
    }
}
