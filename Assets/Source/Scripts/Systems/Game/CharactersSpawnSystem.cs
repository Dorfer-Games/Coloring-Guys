﻿using Kuhpik;
using UnityEngine;

public class CharactersSpawnSystem : GameSystem, IIniting
{
    [SerializeField] Rigidbody character;

    void IIniting.OnInit()
    {
        game.characters = new Rigidbody[1] { character };
        game.charactersRotations = new float[1];
    }
}
