using UnityEngine;

namespace Kuhpik
{
    [CreateAssetMenu(menuName = "Kuhpik/GameConfig")]
    public sealed class GameConfig : ScriptableObject
    {
        [field: SerializeField] public float MoveSpeed;
        [field: SerializeField] public float RotationSpeed;

        public void UpdateMoveSpeed(float value)
        {
            MoveSpeed = value;
        }

        public void UpdateRotationSpeed(float value)
        {
            RotationSpeed = value;
        }
    }
}