using Kuhpik;
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

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            var character = Instantiate(characterPrefab, spawnPoints[i].transform.position, spawnPoints[i].transform.rotation);
            if(i == 0)
            GameObject.FindObjectOfType<ControllerCameraCinemachine>().SetSettingsCamera(character.transform);

            game.characters[i] = new Character();
            game.characters[i].rigidbody = character.GetComponent<Rigidbody>();
            game.characters[i].animator = character.GetComponent<Animator>();
            game.characters[i].color = characterColors[i];
            game.characters[i].onCollisionComponent = character.GetComponent<OnCollisionEnterComponent>();
            game.characters[i].onTriggerComponent = character.GetComponent<OnTriggerEnterComponent>();
            game.characters[i].isPlayer = i == 0;
        }        

        game.characterDictionary = game.characters.ToDictionary(x => x.rigidbody.transform, x => x);
    }
}
