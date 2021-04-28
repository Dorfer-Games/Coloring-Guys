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
    List<ColorStackComponent> colors = new List<ColorStackComponent>();

    void IIniting.OnInit()
    {
        levelInfo = GameObject.FindObjectOfType<LevelInfoComponent>();

        foreach (var character in game.characters)
        {
            character.onTriggerComponent.OnEnter += Collect;
        }

        for (int i = 0; i < 6; i++)
        {
            StartCoroutine(RespawnRoutine(game.cellsList.GetRandom().transform, firstSpawnTime));
        }
    }

    void Collect(Transform other, Transform @object)
    {
        if (other.CompareTag("Color"))
        {
            var character = game.characterDictionary[@object];
            var color = other.GetComponent<ColorStackComponent>();

            colors.Remove(color);
            character.stacks = Mathf.Clamp(character.stacks + color.Count, 0, Mathf.RoundToInt(config.GetValue(EGameValue.ColorMax)));
            StartCoroutine(RespawnRoutine(game.cellsList.GetRandom().transform, config.GetValue(EGameValue.ColorSpawnDelay)));

            color.transform.parent = null;
            PoolingSystem.Pool(color.gameObject);

            Signals.Get<HexCountChangedSignal>().Dispatch(character, character.stacks);
            if (@object.name == "Player")
            {
                AudioSysytem.audioSysytem.AudioCollectStack();
                HapticSystem.hapticSystem.Vibrate();
            }
        }
    }

    void Spawn(Transform spawn)
    {
        PoolingSystem.GetComponent(colorPrefab, out ColorStackComponent component);
        component.Setup(Color.green, Mathf.RoundToInt(config.GetValue(EGameValue.ColorPerStack)), game);
        colors.Add(component);

        component.transform.position = spawn.transform.position + spawnPosition;
        Signals.Get<PlayerNotificationSignal>().Dispatch("Color Spawned!");
        AudioSysytem.audioSysytem.AudioSpawnStack();
    }

    IEnumerator RespawnRoutine(Transform spawnPoint, float time)
    {
        yield return new WaitForSeconds(time);
        Spawn(spawnPoint);
    }
}
