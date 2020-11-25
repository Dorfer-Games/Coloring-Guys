using Kuhpik;
using UnityEngine;

public class CharactersMovingSystem : GameSystem, IFixedUpdating
{
    void IFixedUpdating.OnFixedUpdate()
    {
        for (int i = 0; i < game.characters.Length; i++)
        {
            var rotation = Vector3.up * config.RotationSpeed * game.characters[i].rotationValue * Time.fixedDeltaTime;
            var movement = Vector3.forward * config.MoveSpeed * Time.fixedDeltaTime;

            game.characters[i].rigidbody.transform.Rotate(rotation);
            game.characters[i].rigidbody.transform.Translate(movement, Space.Self);
            game.characters[i].animator.SetFloat("Dir", game.characters[i].rotationValue);
        }
    }
}
