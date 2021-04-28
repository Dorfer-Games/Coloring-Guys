using Kuhpik;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class RandomSkinsData
{
    public StoreItem[] Skins;
}

public class CharactersSpawnSystem : GameSystem, IIniting
{
    [SerializeField] GameObject characterPrefab;
    [SerializeField] Color[] characterColors;

    [Header("Random Characters")]
    [SerializeField] RandomSkinsData[] skinDatas;

    void IIniting.OnInit()
    {
        var spawnPoints = GameObject.Find("Characters SP").GetComponentsInChildren<CharacterSpawnComponent>().OrderBy(x => x.Index).ToArray();
        var colors = characterColors.OrderBy(x => Guid.NewGuid()).ToArray();

        var loopIndex = GameloopExtensions.CalculateLoopIndex(player.level, 5, skinDatas.Length);
        var players = (player.level + 5) % 5;
        players += 3; //Начальное кол-во ботов
        players += 1; //Игрок

        game.characters = new Character[players];
        game.Player = new List<GameObject>();

        for (int i = 0; i < players ; i++) //1 это игрок
        {
            var character = Instantiate(characterPrefab, spawnPoints[i].transform.position, spawnPoints[i].transform.rotation);
            if (i == 0) GameObject.FindObjectOfType<CameraFollow>().SetTarget(character.transform);

            game.characters[i] = new Character();
            game.Player.Add(character);
            game.characters[i].DataAllToPlayer = character.GetComponent<DataAllToPlayerComponent>();
            game.characters[i].rigidbody = character.GetComponent<Rigidbody>();
            game.characters[i].color = colors[i];
            game.characters[i].onCollisionComponent = character.GetComponent<OnCollisionEnterComponent>();
            game.characters[i].onTriggerComponent = character.GetComponent<OnTriggerEnterComponent>();
            game.characters[i].onTriggerEnterImpact = character.GetComponent<OnTriggerEnterImpactComponent>();
            game.characters[i].audioComponent = character.GetComponent<AudioComponent>();
            game.characters[i].jumpPlayerComponent = character.GetComponent<AutoJumpPlayerComponent>();
            game.characters[i].isPlayer = i == 0;

            // Надеваем скинчик
            if (i != 0)
            {
                var skinIndex = Bootstrap.GetSystem<CharactersRandomizeSystem>().GetIndexOfSkin(skinDatas[loopIndex].Skins[i-1]); //Не считая игрока
                game.characters[i].DataAllToPlayer.skin = skinIndex;
                game.characters[i].DataAllToPlayer.EnabledSkinPlayer();
            }
        }

        game.characterDictionary = game.characters.ToDictionary(x => x.rigidbody.transform, x => x);
    }


    public void SetComponentPlayer(int LenghtPlayer)
    {
        for (int i = 0; i < LenghtPlayer; i++)
        {
            game.characters[i].animator = game.characters[i].DataAllToPlayer.Animator();
            game.characters[i].DataAllToPlayer.UpdateBodyColor(characterColors[i]);
        }
    }
}
