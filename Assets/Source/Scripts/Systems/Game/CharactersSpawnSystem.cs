using Kuhpik;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CharactersSpawnSystem : GameSystem, IIniting
{
    [SerializeField] GameObject characterPrefab;
    [SerializeField] Color[] characterColors;

    void IIniting.OnInit()
    {
        var spawnPoints = game.level.transform.Find("Characters SP").GetComponentsInChildren<CharacterSpawnComponent>().OrderBy(x => x.Index).ToArray();
        game.characters = new Character[spawnPoints.Length];

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            
            var character = Instantiate(characterPrefab, spawnPoints[i].transform.position, spawnPoints[i].transform.rotation);

            if (i == 0)
            {
                /*character.transform.position = new Vector3(38, 27, 15);
                Rigidbody rb = character.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.useGravity = false;*/

                GameObject.FindObjectOfType<CameraCinemachineComponent>().SetSettingsCamera(character.transform);
            }

            

            game.characters[i] = new Character();
            game.characters[i].increasedCells = new List<CellComponent>();
            game.characters[i].rigidbody = character.GetComponent<Rigidbody>();
            game.characters[i].animator = character.transform.Find("bandit").GetComponent<Animator>();
            game.characters[i].color = characterColors[i];
            game.characters[i].onCollisionComponent = character.GetComponent<OnCollisionEnterComponent>();
            game.characters[i].onTriggerComponent = character.GetComponent<OnTriggerEnterComponent>();
            game.characters[i].onTriggerEnterImpact = character.GetComponent<OnTriggerEnterImpactComponent>();
            game.characters[i].audioComponent = character.GetComponent<AudioComponent>();
            character.GetComponent<SkinedPlayerComponent>().UpdateBodyColor(characterColors[i]);
            game.characters[i].jumpPlayerComponent = character.GetComponent<AutoJumpPlayerComponent>();
            game.characters[i].isPlayer = i == 0;

            if (i != 0)
            {
                game.characters[i].levelOfMistake = 0;
            }
        }        

        game.characterDictionary = game.characters.ToDictionary(x => x.rigidbody.transform, x => x);
    }
}
