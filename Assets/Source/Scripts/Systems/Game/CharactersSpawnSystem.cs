using Kuhpik;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharactersSpawnSystem : GameSystem, IIniting
{
    [SerializeField] GameObject characterPrefab;
    [SerializeField] Color[] characterColors;

    void IIniting.OnInit()
    {
        var spawnPoints = game.level.transform.Find("Characters SP").GetComponentsInChildren<CharacterSpawnComponent>().OrderBy(x => x.Index).ToArray();
        game.characters = new Character[spawnPoints.Length];
        game.Player = new List<GameObject>();

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            var character = Instantiate(characterPrefab, spawnPoints[i].transform.position, spawnPoints[i].transform.rotation);
            if(i == 0)
                GameObject.FindObjectOfType<CameraFollow>().SetTarget(character.transform);


            game.characters[i] = new Character();
            game.Player.Add(character);
            game.characters[i].DataAllToPlayer = character.GetComponent<DataAllToPlayerComponent>();
            game.characters[i].rigidbody = character.GetComponent<Rigidbody>();
            game.characters[i].color = characterColors[i];
            game.characters[i].onCollisionComponent = character.GetComponent<OnCollisionEnterComponent>();
            game.characters[i].onTriggerComponent = character.GetComponent<OnTriggerEnterComponent>();
            game.characters[i].onTriggerEnterImpact = character.GetComponent<OnTriggerEnterImpactComponent>();
            game.characters[i].audioComponent = character.GetComponent<AudioComponent>();
            game.characters[i].jumpPlayerComponent = character.GetComponent<AutoJumpPlayerComponent>();
            game.characters[i].isPlayer = i == 0;
        }
        
        game.characterDictionary = game.characters.ToDictionary(x => x.rigidbody.transform, x => x);
        Bootstrap.GetSystem<CharactersRandomizeSystem>().UpdateSkins();
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
