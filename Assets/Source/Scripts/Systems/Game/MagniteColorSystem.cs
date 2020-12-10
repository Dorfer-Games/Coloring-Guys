using DG.Tweening;
using Kuhpik;
using NaughtyAttributes;
using UnityEngine;

public class MagniteColorSystem : GameSystem, IIniting
{
    [SerializeField] [Tag] string collisionTag;
    [SerializeField] [BoxGroup("Settings")] float time;
    [SerializeField] [BoxGroup("Settings")] Ease ease;

    void IIniting.OnInit()
    {
        foreach (var character in game.characters)
        {
            character.onTriggerComponent.OnEnter += Magnite;
        }    
    }

    void Magnite(Transform other, Transform @object)
    {
        if (other.CompareTag(collisionTag))
        {
            var position = other.parent.position;
            position.x = 0;
            position.z = 0;

            other.parent.SetParent(@object);
            other.parent.DOLocalMove(position, time).SetEase(ease);
        }
    }
}
