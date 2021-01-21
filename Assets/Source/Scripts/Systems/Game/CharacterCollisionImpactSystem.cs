using Kuhpik;
using NaughtyAttributes;
using UnityEngine;
using System.Collections;
public class CharacterCollisionImpactSystem : GameSystem, IIniting
{
    [SerializeField] [Tag] string collisionTag;

    private bool isCollision = true;


    void IIniting.OnInit()
    {
        foreach (var character in game.characters)
        {
            character.onTriggerEnterImpact.OnEnter += OnPlayerCollision;
        }
    }

    void OnPlayerCollision(Transform other, Transform mainObject)
    {
        if (isCollision && other.transform.CompareTag(collisionTag)) 
        {
            game.characterDictionary[other].rigidbody.velocity = new Vector3(0f,0f,0f);
            var normalized = (other.position - mainObject.position).normalized;
            game.characterDictionary[other].rigidbody.AddForce((normalized * config.GetValue(EGameValue.HitImpulse)) + Vector3.up * (config.GetValue(EGameValue.HitImpulse)  - 30f), ForceMode.Impulse);

            game.characterDictionary[mainObject].rigidbody.velocity = new Vector3(0f, 0f, 0f);
            var normalized_ = (other.position - mainObject.position).normalized;
            game.characterDictionary[mainObject].rigidbody.AddForce(-normalized_ * (config.GetValue(EGameValue.HitImpulse) - 19f), ForceMode.Impulse);
            StartCoroutine(SetCollision());
            Bootstrap.GetSystem<SmilesSystem>().CreateSmiles(other, mainObject);
            if(mainObject.transform.name == "Player")
            AudioSysytem.audioSysytem.AudioCollision();
        }
    }


    private IEnumerator SetCollision()
    {
        isCollision = false;
        yield return new WaitForSeconds(1f);
        isCollision = true;
    }
}
