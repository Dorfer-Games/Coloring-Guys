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

    LevelInfoComponent levelInfo;
    ColorSpawnComponent[] colorSpawnPoints;
    List<ColorStackComponent> colors = new List<ColorStackComponent>();

    void IIniting.OnInit()
    {
        levelInfo = GameObject.FindObjectOfType<LevelInfoComponent>();
        colorSpawnPoints = game.level.transform.Find("Colors SP").GetComponentsInChildren<ColorSpawnComponent>().ToArray();

        foreach (var character in game.characters)
        {
            character.onTriggerComponent.OnEnter += Collect;
        }

        /*foreach (var spawnPoint in colorSpawnPoints)
        {
            StartCoroutine(RespawnRoutine(spawnPoint.transform, firstSpawnTime));
        }*/
    }

    void Collect(Transform other, Transform @object)
    {
        if (other.CompareTag("Color"))
        {
            var character = game.characterDictionary[@object];
            var color = other.GetComponent<ColorStackComponent>();

            colors.Remove(color);
            character.stacks = Mathf.Clamp(character.stacks + color.Count, 0, Mathf.RoundToInt(config.GetValue(EGameValue.ColorMax)));
            StartCoroutine(RespawnRoutine(color.Parent, config.GetValue(EGameValue.ColorSpawnDelay)));

            color.transform.parent = null;
            PoolingSystem.Pool(color.gameObject);

            Signals.Get<HexCountChangedSignal>().Dispatch(character, character.stacks);
            if (@object.name == "Player") {
                AudioSysytem.audioSysytem.AudioCollectStack();
                HapticSystem.hapticSystem.Vibrate();
            }
        }
    }

    void Spawn(Transform spawn)
    {
        //Не делаем переспавн, что бы избежать случаев, когда игрок почти добежал до своей краски, а она появилась в другом месте.
        if (spawn.childCount == 0)
        {
            PoolingSystem.GetComponent(colorPrefab, out ColorStackComponent component);
            component.Setup(component.Parent == null ? spawn : null, Color.green, Mathf.RoundToInt(config.GetValue(EGameValue.ColorPerStack)));
            colors.Add(component);

            component.transform.SetParent(component.Parent);
            component.transform.localPosition = spawnPosition;

            if (Mathf.RoundToInt(config.GetValue(EGameValue.RandomColorSpawn)) == 1)
            {
                component.Parent.localPosition = new Vector3(levelInfo.ColorSpawnAreaX.FromMinMax(), 0, levelInfo.ColorSpawnAreaZ.FromMinMax());
            }

            Signals.Get<PlayerNotificationSignal>().Dispatch("Color Spawned!");
            AudioSysytem.audioSysytem.AudioSpawnStack();
        }
    }

    IEnumerator RespawnRoutine(Transform spawnPoint, float time)
    {
        yield return new WaitForSeconds(time);
        Spawn(spawnPoint);
    }
}
