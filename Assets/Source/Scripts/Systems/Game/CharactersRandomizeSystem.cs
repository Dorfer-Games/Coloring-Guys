using Kuhpik;
using UnityEngine;

public class CharactersRandomizeSystem : GameSystem, IIniting
{
    [SerializeField] Mesh[] characterMeshes;

    void IIniting.OnInit()
    {
        foreach (var character in game.characters)
        {
            character.rigidbody.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = characterMeshes.GetRandom();
        }
    }
}
