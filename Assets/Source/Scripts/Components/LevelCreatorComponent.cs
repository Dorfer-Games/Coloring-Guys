using System;
using UnityEngine;

[Obsolete] //Используется только для первых тестов механики. Далее лвлы должны собираться руками.
public class LevelCreatorComponent : MonoBehaviour
{
    [SerializeField] GameObject cellPrefab;
    [SerializeField] Vector2Int borders;
    [SerializeField] Vector3Int position;
    [SerializeField] int scale;

    void Awake()
    {
        for (int i = 0; i < borders.x; i++)
        {
            for (int j = 0; j < borders.y; j++)
            {
                Instantiate(cellPrefab, new Vector3(i, 0, j), Quaternion.identity, transform);
            }
        }

        transform.localScale = new Vector3(scale, 1, scale);
        transform.position = position;
    }
}
