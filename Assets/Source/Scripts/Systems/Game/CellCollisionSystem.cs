using DG.Tweening;
using Kuhpik;
using Supyrb;
using System.Linq;
using UnityEngine;

public class CellCollisionSystem : GameSystem, IIniting
{
    void IIniting.OnInit()
    {
        foreach (var character in game.characters)
        {
            character.onCollisionComponent.OnEnter += ColorCell;
        }
    }

    void ColorCell(Transform other, Transform @object)
    {
        if (other.CompareTag("Cell"))
        {
            if (DOTween.IsTweening(other)) return;

            var character = game.characterDictionary[@object];
            var component = game.cellDictionary[other.parent];

            if (character.color != component.Color)
            {
                if (character.stacks > 0) ColorCell(character, component);
                else FadeCell(other, component);
            }

            Signals.Get<HexCountChangedSignal>().Dispatch(character, character.stacks);
        }
    }

    private static void ColorCell(Character character, CellComponent component)
    {
        component.SetColor(character.color);
        character.stacks--;
    }

    private void FadeCell(Transform other, CellComponent component)
    {
        var seq = DOTween.Sequence();
        var color = Color.white;

        component.SetDown(true);

        seq.Append(DOTween.To(() => color, x => color = x, Color.red, config.CellFadeTime).OnUpdate(() => component.SetColor(color)).SetEase(Ease.OutCubic));
        seq.Append(other.DOLocalMoveY(-20, config.CellFallTime).SetEase(Ease.Linear));
        seq.OnComplete(() => BringCellBack(other));
        seq.SetId(component.GetInstanceID());
        seq.SetEase(Ease.Linear);
        seq.Play();
    }

    void BringCellBack(Transform cell)
    {
        var component = game.cellDictionary[cell.parent];
        component.SetColor(Color.white);

        cell.DOLocalMoveY(0, config.CellFallTime).SetDelay(config.CellBackTime).SetId(component.GetInstanceID()).OnComplete(() =>
        {
            component.SetDown(false);
        });
    }
}
