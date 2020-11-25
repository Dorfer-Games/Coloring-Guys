using Kuhpik;
using UnityEngine;

public class CharactersMovingSystem : GameSystem, IFixedUpdating
{
    void IFixedUpdating.OnFixedUpdate()
    {
        for (int i = 0; i < game.characters.Length; i++)
        {
            var rotation = Vector3.up * config.RotationSpeed * game.charactersRotations[i] * Time.fixedDeltaTime;
            var movement = Vector3.forward * config.MoveSpeed * Time.fixedDeltaTime;

            game.characters[i].transform.Rotate(rotation);
            game.characters[i].transform.Translate(movement, Space.Self);
            game.characterAnimators[i].SetFloat("Dir", game.charactersRotations[i]);
        }
    }
}
