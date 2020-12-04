using NaughtyAttributes;
using System;
using UnityEngine;

[Obsolete] //Используется только для первых тестов механики. Далее лвлы должны собираться руками.
public class LevelCreatorComponent : MonoBehaviour
{
    [Header("Level")]
    [SerializeField] GameObject cellPrefab;
    [SerializeField] Vector2Int borders;
    [SerializeField] int scale;

    [Header("Cell")]
    [SerializeField] float xOffset;
    [SerializeField] float zOffset;

    [Header("Indexes")]
    [SerializeField] int index;

    [Button]
    void Create()
    {
        var level = new GameObject($"Level {index}");
        var cells = new GameObject("Cells");
        var characterSpawns = new GameObject("Characters SP");
        var colorSpawns = new GameObject("Colors SP");
        var traps = new GameObject("Traps");

        cells.transform.SetParent(level.transform);
        characterSpawns.transform.SetParent(level.transform);
        colorSpawns.transform.SetParent(level.transform);
        traps.transform.SetParent(level.transform);

        for (int i = 0; i < borders.y; i++)
        {
            var additionalXoffset = (i % 2) * (xOffset / 2f);

            for (int j = 0; j < borders.x; j++)
            {
                float x = xOffset * j + additionalXoffset;
                float z = zOffset * i;

                Instantiate(cellPrefab, new Vector3(x, 0, z), Quaternion.identity, cells.transform);
            }
        }

        level.transform.localScale = new Vector3(scale, 1, scale);
    }
}
