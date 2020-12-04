using Kuhpik;
using System.Linq;
using UnityEngine;

public class ColorSpawningSystem : GameSystem, IIniting
{
    [SerializeField] GameObject colorPrefab;
    [SerializeField] Vector3 spawnPosition;

    void IIniting.OnInit()
    {
        foreach (var character in game.characters)
        {
            character.onTriggerComponent.OnEnter += Collect;
        }
    }

    void Collect(Transform other, Transform @object)
    {
        if (other.CompareTag("Color")) 
        {
            var character = game.characterDictionary[@object];
            var color = other.GetComponent<ColorStackComponent>();

            if (color.Color == character.color)
            {
                character.stacks += color.Count;
                Destroy(color.gameObject);
            }
        }
    }
}
