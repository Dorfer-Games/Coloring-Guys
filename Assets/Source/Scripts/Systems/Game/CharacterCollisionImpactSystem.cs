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
            character.onTriggerEnterImpact.OnEnter += OnPlayerCollision;
        }
    }

    void OnPlayerCollision(Transform other)
    {
        if (other.transform.CompareTag(collisionTag)) 
        {
            var normalized = other.position.normalized;
            game.characterDictionary[other].rigidbody.AddForce((normalized + transform.forward) * config.GetValue(EGameValue.HitImpulse), ForceMode.Impulse);
            AudioSysytem.audioSysytem.AudioCollision();
        }
    }
}
