using Kuhpik;
using Kuhpik.Pooling;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColorSpawningSystem : GameSystem, IIniting
{
    [SerializeField] GameObject colorPrefab;
    [SerializeField] Vector3 spawnPosition;
    [SerializeField] float respawnTime;
    [SerializeField] int colorPerStack;

    ColorSpawnComponent[] colorSpawnPoints;
    List<ColorStackComponent> colors = new List<ColorStackComponent>();

    void IIniting.OnInit()
    {
        colorSpawnPoints = game.level.transform.Find("Colors SP").GetComponentsInChildren<ColorSpawnComponent>().ToArray();

        foreach (var character in game.characters)
        {
            character.onTriggerComponent.OnEnter += Collect;
        }

        foreach (var spawnPoint in colorSpawnPoints)
        {
            StartCoroutine(RespawnRoutine(spawnPoint.transform));
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
                colors.Remove(color);
                character.stacks += color.Count;
                StartCoroutine(RespawnRoutine(color.transform.parent));

                color.transform.parent = null;
                PoolingSystem.Pool(color.gameObject);
            }
        }
    }

    void Spawn(Transform spawn)
    {
        //Не делаем переспавн, что бы избежать случаев, когда игрок почти добежал до своей краски, а она появилась в другом месте.
        if (spawn.childCount == 0)
        {
            var color = game.characters[0].color;
            //Нужно, что бы у игрока всегда была краска.
            if (colors.Any(x => x.Color == game.characters[0].color)) 
            {
                var colorsLeft = game.characters.Select(x => x.color).Except(colors.Select(x => x.Color));
                color = colorsLeft.ToArray().GetRandom();
            }

            PoolingSystem.GetComponent(colorPrefab, out ColorStackComponent component);
            component.Setup(color, colorPerStack);
            colors.Add(component);

            component.transform.SetParent(spawn);
            component.transform.localPosition = spawnPosition;
        }
    }

    IEnumerator RespawnRoutine(Transform spawnPoint)
    {
        yield return new WaitForSeconds(respawnTime);
        Spawn(spawnPoint);
    }
}
