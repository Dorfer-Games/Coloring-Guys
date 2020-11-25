using Kuhpik;
using UnityEngine;

public class CharactersSpawnSystem : GameSystem, IIniting
{
    [SerializeField] Rigidbody character;

    void IIniting.OnInit()
    {
        game.characters = new Character[1] { new Character() };
        game.characters[0].rigidbody = character;
        game.characters[0].animator = character.GetComponent<Animator>();
        game.characters[0].color = Color.yellow;
    }
}
