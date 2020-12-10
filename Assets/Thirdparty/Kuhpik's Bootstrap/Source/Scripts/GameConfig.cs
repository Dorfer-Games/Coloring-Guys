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
        [field: SerializeField] public float JumpStrength { get; private set; }
        [field: SerializeField] public float GravityStrength { get; private set; }
        [field: SerializeField] public int ColorPerStack { get; private set; }
        [field: SerializeField] public int ColorMax { get; private set; }

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

        public void UpdateJumpStrength(float value)
        {
            JumpStrength = value;
        }

        public void UpdateGravityStrenght(float value)
        {
            GravityStrength = value;
        }

        public void UpdateColorPerStack(float value)
        {
            ColorPerStack = Mathf.RoundToInt(value);
        }

        public void UpdateColorMax(float value)
        {
            ColorMax = Mathf.RoundToInt(value);
        }
    }
}