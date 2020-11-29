using Kuhpik;
using System.Linq;
using UnityEngine;

public class CharactersSpawnSystem : GameSystem, IIniting
{
    [Header("Игрок должен быть на индексе 0")]
    [SerializeField] Rigidbody[] characters;
    [SerializeField] Color[] characterColors;

    void IIniting.OnInit()
    {
        game.characters = new Character[characters.Length];

        for (int i = 0; i < characters.Length; i++)
        {
            game.characters[i] = new Character();
            game.characters[i].rigidbody = characters[i];
            game.characters[i].animator = characters[i].GetComponent<Animator>();
            game.characters[i].color = characterColors[i];
            game.characters[i].onCollisionComponent = characters[i].GetComponent<OnCollisionEnterComponent>();
            game.characters[i].onTriggerComponent = characters[i].GetComponent<OnTriggerEnterComponent>();
        }        

        game.characterDictionary = game.characters.ToDictionary(x => x.rigidbody.transform, x => x);
    }
}
