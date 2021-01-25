﻿using Kuhpik;
using Supyrb;
using DG.Tweening;
using System;
using System.Linq;
using UnityEngine;

public class DeathCollisionSystem : GameSystem, IIniting
{
    [SerializeField] private GameObject VFXExplosionEffectsPrefab;
    void IIniting.OnInit()
    {
        foreach (var character in game.characters)
        {
            character.onTriggerComponent.OnEnter += Death;
        }
    }

    void Death(Transform other, Transform @object)
    {
        if (other.CompareTag("Death") || other.CompareTag("Cell") && game.cellDictionary[other.parent].IsUp)
        {
            var character = game.characterDictionary[@object];
            character.rigidbody.gameObject.SetActive(false);
            character.isDeath = true;
            PlayVFX(@object);
            if (other.CompareTag("Cell"))
            {
                ColorCellAround(other, @object);
            }

            if (character == game.characters[0])
            {
                game.isVictory = false;
                Bootstrap.ChangeGameState(EGamestate.Finish);
                AudioSysytem.audioSysytem.AudioDefeat();
                game.characters[0].audioComponent.DisabledAudio();
            }

            else
            {
                Signals.Get<PlayerNotificationSignal>().Dispatch($"{@object.name} is going down!");
                AudioSysytem.audioSysytem.AudioDead();

                if (game.characters.Count(x => x.isDeath) == game.characters.Length - 1)
                {
                    game.isVictory = true;
                    Bootstrap.ChangeGameState(EGamestate.Finish);
                    AudioSysytem.audioSysytem.AudioVictory();
                    game.characters[0].audioComponent.DisabledAudio();
                }
            }
            Bootstrap.GetSystem<SmilesSystem>().CreateSmiles(@object, character.onTriggerEnterImpact.lastCollisionPlayer, false);
        }
    }

    private void ColorCellAround(Transform cell, Transform character)
    {
        
        var cellComponent = game.cellDictionary[cell.parent];
        var characterComponent = game.characterDictionary[character];
        ColorCell(game.cellDictionary[cell.parent], characterComponent);
        foreach(CellComponent cellComponent1 in cellComponent.GetCellsAround())
        {
            ColorCell(cellComponent1, characterComponent);
        }
    }

    private void ColorCell(CellComponent cellComponent, Character characterComponent)
    {
        cellComponent.SetUp(false);
        cellComponent.IsGoingToGoUp = false;
        cellComponent.SetColor(characterComponent.color);
        cellComponent.Cell.transform.DOLocalMoveY(config.GetValue(EGameValue.CellUpY), 0f);
    }

    private void PlayVFX(Transform character)
    {
        GameObject VFXobject = Instantiate(VFXExplosionEffectsPrefab, character.position, Quaternion.identity);
        VFXobject.transform.localScale = new Vector3(10f,10f,10f);
        ParticleSystem particleSystem = VFXobject.GetComponent<ParticleSystem>();
        particleSystem.startColor = game.characterDictionary[character].color;
        particleSystem.Play();
        //Destroy(VFXobject, 2f);
    }
}
