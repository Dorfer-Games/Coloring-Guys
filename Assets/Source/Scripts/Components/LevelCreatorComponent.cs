using NaughtyAttributes;
using System;
using UnityEditor;
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

#if UNITY_EDITOR

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

                var cell = PrefabUtility.InstantiatePrefab(cellPrefab) as GameObject;
                cell.transform.SetParent(cells.transform);
                cell.transform.localPosition = new Vector3(x, 0, z);

            }
        }

        level.transform.localScale = new Vector3(scale, 1, scale);
    }


    [Button]
    void CreateNewTypeLevel() // Триангл тип создания хексов
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
            var additionalXoffset = (i % 1) * (xOffset / 1f);

            for (int j = 0; j < borders.x; j++)
            {
                float x = xOffset * j + additionalXoffset;
                float z = zOffset * i;
                for (int b = 0; b <= 1; b++) {
                    if (b == 0)
                    {
                        var cell = PrefabUtility.InstantiatePrefab(cellPrefab) as GameObject;
                        cell.transform.SetParent(cells.transform);
                        cell.transform.localPosition = new Vector3(x, 0, z);
                    }
                    else
                    {
                        var cell = PrefabUtility.InstantiatePrefab(cellPrefab) as GameObject;
                        cell.transform.SetParent(cells.transform);
                        cell.transform.localPosition = new Vector3(x, 0, z);
                        cell.transform.eulerAngles = new Vector3(0,180f,0);
                    }
                }

            }
        }

        level.transform.localScale = new Vector3(scale, 1, scale);
    }
#endif
}
