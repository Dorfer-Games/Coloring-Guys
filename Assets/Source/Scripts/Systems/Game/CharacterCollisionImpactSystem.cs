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

    void OnPlayerCollision(Transform other, Transform mainObject)
    {
        if (other.transform.CompareTag(collisionTag)) 
        {
            var normalized = (other.position - mainObject.position).normalized;
            game.characterDictionary[other].rigidbody.AddForce(normalized * config.GetValue(EGameValue.HitImpulse), ForceMode.Impulse);

           /* var normalized_ = mainObject.position.normalized;
            game.characterDictionary[mainObject].rigidbody.AddForce((normalized_ - transform.forward) * (config.GetValue(EGameValue.HitImpulse) - 10f), ForceMode.Impulse);*/
            if(mainObject.transform.name == "Player")
            AudioSysytem.audioSysytem.AudioCollision();
        }
    }
}
