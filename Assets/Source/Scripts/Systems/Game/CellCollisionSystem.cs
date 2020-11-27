﻿using DG.Tweening;
using Kuhpik;
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
        component.SetDown(true);

        seq.Append(renderer.material.DOColor(Color.red, config.CellFadeTime).SetEase(Ease.OutCubic));
        seq.Append(other.DOLocalMoveY(-20, config.CellFallTime).SetEase(Ease.Linear));
        seq.OnComplete(() => BringCellBack(component));
        seq.SetId(component.GetInstanceID());
        seq.SetEase(Ease.Linear);
        seq.Play();
    }

    void BringCellBack(CellComponent component)
    {
        component.Renderer.material.color = Color.white;
        component.SetColor(Color.white);

        component.Cell.transform.DOLocalMoveY(0, config.CellFallTime).SetDelay(config.CellBackTime).SetEase(Ease.Linear).SetId(component.GetInstanceID()).OnComplete(() =>
        {
            component.SetDown(false);
        });
    }
}
