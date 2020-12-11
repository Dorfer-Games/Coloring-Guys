using UnityEngine;

public class LevelInfoComponent : MonoBehaviour
{
    [field: SerializeField] public Vector2 ColorSpawnAreaX { get; private set; }
    [field: SerializeField] public Vector2 ColorSpawnAreaZ { get; private set; }
}
