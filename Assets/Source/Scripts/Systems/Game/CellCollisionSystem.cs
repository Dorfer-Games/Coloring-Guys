using DG.Tweening;
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
                else FadeCell(other, component, @object);
            }

            Signals.Get<HexCountChangedSignal>().Dispatch(character, character.stacks);
        }
    }

    private static void ColorCell(Character character, CellComponent component)
    {
        component.SetColor(character.color);
        character.stacks--;
    }

    private void FadeCell(Transform cell, CellComponent component, Transform character)
    {
        var seq = DOTween.Sequence();
        var color = Color.white;

        //component.SetUp(true);

        seq.Append(DOTween.To(() => color, x => color = x, Color.red, config.GetValue(EGameValue.CellFadeTime)).OnUpdate(() => component.SetColor(color)).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            component.SetUp(true);
        }));
        //seq.PrependInterval(config.GetValue(EGameValue.CellFadeTime));
        //seq.Append(other.DOLocalMoveY(4.4f, config.GetValue(EGameValue.CellFallTime)).SetEase(Ease.Linear));
        //seq.OnComplete(() => BringCellBack(other));
        seq.OnComplete(() => StartCoroutine(CellMoving(cell, character)));

        seq.SetId(component.GetInstanceID());
        seq.SetEase(Ease.Linear);
        seq.Play();
    }


    IEnumerator CellMoving(Transform cell, Transform character)
    {
        float needHight = 4.4f;
        float baseHexIncreaseSpeed = config.GetValue(EGameValue.CellIncreaseSpeed);
        float distnaceBetweenCellAndCharacter = GetDistance(character, cell);
        float cellSize = 1f;
        while (distnaceBetweenCellAndCharacter <= 3f || cell.transform.position.y < needHight)
        {
            distnaceBetweenCellAndCharacter = GetDistance(character, cell);
            cell.transform.position += new Vector3(0, baseHexIncreaseSpeed * (distnaceBetweenCellAndCharacter / (3f * cellSize)),0);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        cell.transform.position = new Vector3(cell.transform.position.x, needHight, cell.transform.position.z);
        BringCellBack(cell);
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

        cell.DOLocalMoveY(config.GetValue(EGameValue.CellUpY), config.GetValue(EGameValue.CellIncreaseSpeed)).SetDelay(config.GetValue(EGameValue.CellBackTime)).SetId(component.GetInstanceID()).OnComplete(() =>
        {
            component.SetUp(false);
            component.SetColor(Color.white);
        });
    }
}
