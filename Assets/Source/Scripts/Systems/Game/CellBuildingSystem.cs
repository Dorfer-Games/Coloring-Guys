using DG.Tweening;
using Kuhpik;
using System.Linq;
using UnityEngine;

public class CellBuildingSystem : GameSystem, IIniting
{
    void IIniting.OnInit()
    {
        foreach (var character in game.characters)
        {
            character.rigidbody.GetComponent<OnTriggerEnterComponent>().OnEnter += Build;
        }
    }

    void Build(Transform other, Transform moving)
    {
        if (other.CompareTag("Cell"))
        {
            var character = game.characters.First(x => x.rigidbody.transform == moving);
            var component = other.parent.GetComponent<CellComponent>();

            if (component.IsDown && character.stacks > 0)
            {
                if (DOTween.IsTweening(component.Cell))
                {
                    DOTween.Kill(component.Cell);
                }

                character.stacks--;
                component.SetDown(false);
                component.SetColor(character.color);
                component.Cell.transform.DOMoveY(0, 0);
                component.Renderer.material.color = character.color;
            }
        }
    }
}
