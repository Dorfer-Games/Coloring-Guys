﻿using DG.Tweening;
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
                if (character.stacks > 0)
                {
                    ColorCell(character, component);
                }
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

        //component.SetUp(true);

        seq.Append(DOTween.To(() => color, x => color = x, Color.red, config.GetValue(EGameValue.CellFadeTime)).OnUpdate(() => component.SetColor(color)).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            component.SetUp(true);
        }));
        //seq.PrependInterval(config.GetValue(EGameValue.CellFadeTime));
        seq.Append(other.DOLocalMoveY(4.4f, config.GetValue(EGameValue.CellFallTime)).SetEase(Ease.Linear));
        seq.OnComplete(() => BringCellBack(other));
        seq.SetId(component.GetInstanceID());
        seq.SetEase(Ease.Linear);
        seq.Play();
    }

    void BringCellBack(Transform cell)
    {
        var component = game.cellDictionary[cell.parent];
        component.SetColor(Color.red);

        cell.DOLocalMoveY(config.GetValue(EGameValue.CellUpY), config.GetValue(EGameValue.CellFallTime)).SetDelay(config.GetValue(EGameValue.CellBackTime)).SetId(component.GetInstanceID()).OnComplete(() =>
        {
            component.SetUp(false);
            component.SetColor(Color.white);
        });
    }
}
