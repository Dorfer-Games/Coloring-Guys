﻿using DG.Tweening;
using Kuhpik;
using Supyrb;
using System.Linq;
using UnityEngine;
using System.Collections;

public class CellCollisionSystem : GameSystem, IIniting
{
    void IIniting.OnInit()
    {
        foreach (var character in game.characters)
        {
            character.onCollisionComponent.OnEnter += ColorCell;
            character.onCollisionComponent.OnEnter += SetPlayersCells;
        }
    }

    void SetPlayersCells(Transform other, Transform @object)
    {
        if (other.CompareTag("Cell"))
        {

            var cellComponent = game.cellDictionary[other.parent];
            var characterComponent = game.characterDictionary[@object];

            if (cellComponent.CharacterWhoCollored == null)
            {
                cellComponent.CharacterWhoCollored = characterComponent;
                characterComponent.increasedCells.Add(cellComponent);
            }
        }
        
    }


    void ColorCell(Transform other, Transform @object)
    {
        if (other.CompareTag("Cell"))
        {
            var cellComponent = game.cellDictionary[other.parent];
            var characterComponent = game.characterDictionary[@object];

            if (DOTween.IsTweening(other) || cellComponent.IsGoingToGoUp) return;


            if (characterComponent.color != cellComponent.Color)
            {
                if (characterComponent.stacks > 0)
                {
                    ColorCell(characterComponent, cellComponent);
                }
                else if (characterComponent.canIncreaseCells)
                {
                    FadeCell(other, cellComponent, @object);
                }

            }

            Signals.Get<HexCountChangedSignal>().Dispatch(characterComponent, characterComponent.stacks);
        }
    }

    private static void ColorCell(Character character, CellComponent component)
    {
        
        component.SetColor(character.color);
        character.stacks--;
    }

    private void FadeCell(Transform cell, CellComponent component, Transform character)
    {
        //var seq = DOTween.Sequence();
        var color = Color.white;

        //component.SetUp(true);

        component.IsGoingToGoUp = true;

        //seq.Append(DOTween.To(() => color, x => color = x, Color.red, config.GetValue(EGameValue.CellFadeTime)).OnUpdate(() => component.SetColor(color)).SetEase(Ease.OutCubic));
        StartCoroutine(CellColorTransition(cell, component, character));
        //seq.PrependInterval(config.GetValue(EGameValue.CellFadeTime));
        //seq.Append(cell.DOLocalMoveY(4.4f, config.GetValue(EGameValue.CellIncreaseTime)).SetEase(Ease.Linear));
        //seq.OnComplete(() => BringCellBack(cell));
        //seq.OnComplete(() => StartCoroutine(CellMoving(cell, component, character)));

        /*seq.SetId(component.GetInstanceID());
        seq.SetEase(Ease.Linear);
        seq.Play();*/
    }

    IEnumerator CellColorTransition(Transform cell, CellComponent component, Transform character)
    {
        Color currentColor = component.Color;
        Color needColor = Color.red;
        Vector4 stepColor = needColor - currentColor;
        Vector4 vectorsDif = (Vector4)needColor - (Vector4)component.Color;
        while (vectorsDif.x + vectorsDif.y + vectorsDif.z < 0 && component.IsGoingToGoUp != false)
        {
            component.SetColor((Vector4)component.Color + stepColor * Time.deltaTime);
            vectorsDif = (Vector4)needColor - (Vector4)component.Color;
            yield return null;
        }
        component.SetColor(needColor);
        StartCoroutine(CellMoving(cell, component, character));
    }

    IEnumerator CellMoving(Transform cell, CellComponent component, Transform character)
    {
        float needHight = 4.4f;
        float baseHexIncreaseSpeed = needHight / config.GetValue(EGameValue.CellIncreaseTime) ;
        float distnaceBetweenCellAndCharacter = GetDistance(character, cell);
        float cellSize = 1f;
        float startCellYAxisPos = cell.transform.position.y;
        while (cell.transform.position.y < needHight && component.IsGoingToGoUp != false)
        {
            if (cell.transform.position.y - startCellYAxisPos >= 0.2f * needHight)
            {
                component.SetUp(true);
            }
            distnaceBetweenCellAndCharacter = GetDistance(character, cell);
            cell.transform.position += Vector3.up * baseHexIncreaseSpeed * Time.deltaTime* Mathf.Clamp(distnaceBetweenCellAndCharacter / (3f * cellSize), 0f, 1f);
            yield return null;
        }
        if (component.IsGoingToGoUp)
        {
            cell.transform.position = new Vector3(cell.transform.position.x, needHight, cell.transform.position.z);
            BringCellBack(cell);
        }
        else
        {
            component.SetUp(false);
            component.IsGoingToGoUp = false;
            component.SetColor(Color.white);
        }
    }

    private static float GetDistance(Transform character, Transform cell)
    {
        return Vector2.Distance(new Vector2(character.position.x, character.position.z),
                    new Vector2(cell.position.x, cell.position.z));
    }

    void BringCellBack(Transform cell)
    {
        var component = game.cellDictionary[cell.parent];
        component.SetColor(Color.red);

        cell.DOLocalMoveY(config.GetValue(EGameValue.CellUpY), config.GetValue(EGameValue.CellIncreaseTime)).SetDelay(config.GetValue(EGameValue.CellBackTime)).SetId(component.GetInstanceID()).OnComplete(() =>
        {
            component.SetUp(false);
            component.IsGoingToGoUp = false;
            component.SetColor(Color.white);
        });
    }
}
