using Kuhpik;
using NaughtyAttributes;
using UnityEngine;

public class CharacterCollisionImpactSystem : GameSystem, IIniting
{
    [SerializeField] [Tag] string collisionTag;

    void IIniting.OnInit()
    {
        foreach (var character in game.characters)
        {
            character.onCollisionComponent.OnEnter += OnPlayerCollision;
        }
    }

    void OnPlayerCollision(Transform other, Transform @object)
    {
        if (other.transform.CompareTag(collisionTag)) 
        {
            var normalized = (other.position - @object.position).normalized;
            game.characterDictionary[other].rigidbody.AddForce(normalized * config.HitImpulse, ForceMode.Impulse);
        }
    }
}
