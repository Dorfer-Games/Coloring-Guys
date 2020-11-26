using System;
using UnityEngine;

[Obsolete] //Используется только для первых тестов механики. Далее лвлы должны собираться руками.
public class LevelCreatorComponent : MonoBehaviour
{
    [Header("Level")]
    [SerializeField] GameObject cellPrefab;
    [SerializeField] Vector2Int borders;
    [SerializeField] Vector3Int position;
    [SerializeField] int scale;

    [Header("Cell")]
    [SerializeField] float xOffset;
    [SerializeField] float zOffset;

    void Awake()
    {
        for (int i = 0; i < borders.y; i++)
        {
            var additionalXoffset = (i % 2) * (xOffset / 2f);
            Debug.Log(additionalXoffset);

            for (int j = 0; j < borders.x; j++)
            {
                float x = xOffset * j + additionalXoffset;
                float z = zOffset * i;

                Instantiate(cellPrefab, new Vector3(x, 0, z), Quaternion.identity, transform);
            }
        }

        transform.localScale = new Vector3(scale, 1, scale);
        transform.position = position;
    }
}
