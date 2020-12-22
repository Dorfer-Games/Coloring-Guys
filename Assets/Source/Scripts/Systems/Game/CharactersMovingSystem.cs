using Kuhpik;
using NaughtyAttributes;
using UnityEngine;

public class CharactersMovingSystem : GameSystem, IFixedUpdating
{
    [SerializeField] [BoxGroup("Settings")] bool speedupMainCharacter;


    void IFixedUpdating.OnFixedUpdate()
    {
        for (int i = 0; i < game.characters.Length; i++)
        {
            var rotation = Vector3.up * config.GetValue(EGameValue.RotationSpeed) * game.characters[i].rotationValue * Time.fixedDeltaTime;
            var movement = Vector3.forward * config.GetValue(EGameValue.MoveSpeed) * Time.fixedDeltaTime;

            if (speedupMainCharacter && game.characters[i].isPlayer) movement *= config.GetValue(EGameValue.PlayerSpeedX);

            game.characters[i].rigidbody.transform.Rotate(rotation);
            game.characters[i].rigidbody.transform.Translate(movement, Space.Self);
            game.characters[i].animator.SetFloat("Dir", game.characters[i].rotationValue);
        }
    }
}
