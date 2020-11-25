using UnityEngine;

namespace Kuhpik
{
    [CreateAssetMenu(menuName = "Kuhpik/GameConfig")]
    public sealed class GameConfig : ScriptableObject
    {
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public float RotationSpeed { get; private set; }
        [field: SerializeField] public float CellFadeTime { get; private set; }
        [field: SerializeField] public float CellFallTime { get; private set; }
        [field: SerializeField] public float CellBackTime { get; private set; }

        public void UpdateMoveSpeed(float value)
        {
            MoveSpeed = value;
        }

        public void UpdateRotationSpeed(float value)
        {
            RotationSpeed = value;
        }

        public void UpdateCellFadeTime(float value)
        {
            CellFadeTime = value;
        }

        public void UpdateCellFallTime(float value)
        {
            CellFallTime = value;
        }

        public void UpdateCellBackTime(float value)
        {
            CellBackTime = value;
        }
    }
}