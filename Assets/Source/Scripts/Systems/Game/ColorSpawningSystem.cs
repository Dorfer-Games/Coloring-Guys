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
            character.rigidbody.GetComponent<OnTriggerEnterComponent>().OnEnter += Collect;
        }

        var colorStack = Instantiate(colorPrefab, spawnPosition, Quaternion.identity).GetComponent<ColorStackComponent>();
        colorStack.Renderer.material.color = Color.yellow;
        colorStack.Setup(30);
    }

    void Collect(Transform other, Transform moving)
    {
        if (other.CompareTag("Color")) 
        {
            var character = game.characters.First(x => x.rigidbody.transform == moving);
            var color = other.GetComponent<ColorStackComponent>();

            if (color.Color == character.color)
            {
                character.stacks += color.Count;
                Destroy(color.gameObject);
            }
        }
    }
}
