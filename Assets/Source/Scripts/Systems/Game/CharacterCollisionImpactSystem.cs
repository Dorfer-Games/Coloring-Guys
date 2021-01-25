using Kuhpik;
using NaughtyAttributes;
using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;

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
            SetDownCells(game.characterDictionary[other]);
            SetDownCells(game.characterDictionary[mainObject]);


            game.characterDictionary[other].rigidbody.velocity = new Vector3(0f,0f,0f);
            var normalized = (other.position - mainObject.position).normalized;
            game.characterDictionary[other].rigidbody.AddForce((normalized * config.GetValue(EGameValue.HitImpulse)) + Vector3.up * (config.GetValue(EGameValue.HitImpulse)  - 30f), ForceMode.Impulse);

            game.characterDictionary[mainObject].rigidbody.velocity = new Vector3(0f, 0f, 0f);
            var normalized_ = (other.position - mainObject.position).normalized;
            game.characterDictionary[mainObject].rigidbody.AddForce(-normalized_ * (config.GetValue(EGameValue.HitImpulse) - 19f), ForceMode.Impulse);
            StartCoroutine(SetCollision());
            Bootstrap.GetSystem<SmilesSystem>().CreateSmiles(other, mainObject, true);
            game.characterDictionary[other].onTriggerEnterImpact.SetLastPlayer(mainObject);
            if (mainObject.transform.name == "Player")
            AudioSysytem.audioSysytem.AudioCollision();
        }
    }

    private void SetDownCells(Character character)
    {
        var cells = character.increasedCells;
        foreach(CellComponent cellComponent in cells)
        {
            if (cellComponent.Color == character.color)
            {
                continue;
            }
            cellComponent.SetUp(false);
            cellComponent.IsGoingToGoUp = false;
            cellComponent.Cell.transform.DOLocalMoveY(config.GetValue(EGameValue.CellUpY), 0f);
            cellComponent.CharacterWhoCollored = null;
            cellComponent.SetColor(Color.white);
        }
        character.increasedCells.Clear();
    }

    private IEnumerator SetCollision()
    {
        isCollision = false;
        yield return new WaitForSeconds(1f);
        isCollision = true;
    }
}
