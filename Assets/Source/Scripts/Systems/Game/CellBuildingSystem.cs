using DG.Tweening;
using Kuhpik;
using Supyrb;
using UnityEngine;

public class CellBuildingSystem : GameSystem, IIniting
{
    void IIniting.OnInit()
    {
        foreach (var character in game.characters)
        {
            character.onTriggerComponent.OnEnter += Build;
        }
    }

    void Build(Transform other, Transform @object)
    {
        if (other.CompareTag("Cell"))
        {
            var character = game.characterDictionary[@object];
            var component = game.cellDictionary[other.parent];
            try
            {
                if (component.IsUp && character.stacks > 0)
                {
                    if (DOTween.IsTweening(component.GetInstanceID()))
                    {
                        DOTween.Kill(component.GetInstanceID());
                    }

                    character.stacks--;
                    component.SetUp(false);
                    component.IsGoingToGoUp = false;
                    component.SetColor(character.color);
                    component.Cell.transform.DOLocalMoveY(config.GetValue(EGameValue.CellUpY),0f);

                    if (character == game.characters[0]) Signals.Get<HexCountChangedSignal>().Dispatch(character, character.stacks);
                }
            }

            catch
            {

            }
        }
    }
}
