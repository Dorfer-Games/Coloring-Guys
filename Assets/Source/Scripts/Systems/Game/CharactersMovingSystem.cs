using Kuhpik;
using NaughtyAttributes;
using UnityEngine;

public class CharactersMovingSystem : GameSystem, IFixedUpdating
{
    [SerializeField] [BoxGroup("Settings")] bool speedupMainCharacter;
    [SerializeField] [BoxGroup("Settings")] [ShowIf("speedupMainCharacter")] float speedMult;

    void IFixedUpdating.OnFixedUpdate()
    {
        for (int i = 0; i < game.characters.Length; i++)
        {
            var rotation = Vector3.up * config.RotationSpeed * game.characters[i].rotationValue * Time.fixedDeltaTime;
            var movement = Vector3.forward * config.MoveSpeed * Time.fixedDeltaTime;

            if (speedupMainCharacter) movement *= speedMult;

            game.characters[i].rigidbody.transform.Rotate(rotation);
            game.characters[i].rigidbody.transform.Translate(movement, Space.Self);
            game.characters[i].animator.SetFloat("Dir", game.characters[i].rotationValue);
        }
    }
}
