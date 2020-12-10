using Kuhpik;
using Kuhpik.Pooling;
using Supyrb;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColorSpawningSystem : GameSystem, IIniting
{
    [SerializeField] GameObject colorPrefab;
    [SerializeField] Vector3 spawnPosition;
    [SerializeField] float firstSpawnTime;
    [SerializeField] float respawnTime;

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
            StartCoroutine(RespawnRoutine(spawnPoint.transform, firstSpawnTime));
        }
    }

    void Collect(Transform other, Transform @object)
    {
        if (other.CompareTag("Color"))
        {
            var character = game.characterDictionary[@object];
            var color = other.GetComponent<ColorStackComponent>();

            //if (color.Color == character.color)
            //{
                colors.Remove(color);
                character.stacks = Mathf.Clamp(character.stacks + color.Count, 0, config.ColorMax);
                StartCoroutine(RespawnRoutine(color.Parent, respawnTime));

                color.transform.parent = null;
                PoolingSystem.Pool(color.gameObject);

                if (character == game.characters[0])
                {
                    Signals.Get<HexCountChangedSignal>().Dispatch(character.stacks);
                    Signals.Get<ColorPickedupSignal>().Dispatch(0);
                }
            //}
        }
    }

    void Spawn(Transform spawn)
    {
        //Не делаем переспавн, что бы избежать случаев, когда игрок почти добежал до своей краски, а она появилась в другом месте.
        if (spawn.childCount == 0)
        {
            //var color = game.characters[0].color;
            //Нужно, что бы у игрока всегда была краска.
            //if (colors.Any(x => x.Color == game.characters[0].color))
            //{
            //    var colorsLeft = game.characters.Select(x => x.color).Except(colors.Select(x => x.Color));
            //    color = colorsLeft.ToArray().GetRandom();
            //}

            PoolingSystem.GetComponent(colorPrefab, out ColorStackComponent component);
            component.Setup(component.Parent == null ? spawn : null, Color.yellow, config.ColorPerStack);
            colors.Add(component);

            component.transform.SetParent(component.Parent);
            component.transform.localPosition = spawnPosition;

            //if (color == game.characters[0].color)
            //{
            //    Signals.Get<PlayerNotificationSignal>().Dispatch("Color Spawned!");
            //    Signals.Get<ColorSpawnedSignal>().Dispatch(component.Parent, 0);
            //}
        }
    }

    IEnumerator RespawnRoutine(Transform spawnPoint, float time)
    {
        yield return new WaitForSeconds(time);
        Spawn(spawnPoint);
    }
}
