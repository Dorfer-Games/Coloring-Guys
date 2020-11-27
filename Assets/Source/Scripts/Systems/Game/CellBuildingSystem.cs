using DG.Tweening;
using Kuhpik;
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

            if (component.IsDown && character.stacks > 0)
            {
                if (DOTween.IsTweening(component.GetInstanceID()))
                {
                    DOTween.Kill(component.GetInstanceID());
                    Debug.Log("Kill");
                }

                character.stacks--;
                component.SetDown(false);
                component.SetColor(character.color);
                component.Renderer.material.color = character.color;
                component.Cell.transform.localPosition = Vector2.zero;
            }
        }
    }
}
