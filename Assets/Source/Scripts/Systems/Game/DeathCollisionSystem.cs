using Kuhpik;
using Supyrb;
using System.Linq;
using UnityEngine;

public class DeathCollisionSystem : GameSystem, IIniting
{
    void IIniting.OnInit()
    {
        foreach (var character in game.characters)
        {
            character.onTriggerComponent.OnEnter += Death;
        }
    }

    void Death(Transform other, Transform @object)
    {
        if (other.CompareTag("Death"))
        {
            var character = game.characterDictionary[@object];
            character.rigidbody.gameObject.SetActive(false);
            character.isDeath = true;

            if (character == game.characters[0])
            {
                game.isVictory = false;
                Bootstrap.ChangeGameState(EGamestate.Finish);
            }

            else
            {
                Signals.Get<PlayerNotificationSignal>().Dispatch($"{@object.name} is going down!");

                if (game.characters.Count(x => x.isDeath) == game.characters.Length - 1)
                {
                    game.isVictory = true;
                    Bootstrap.ChangeGameState(EGamestate.Finish);
                }
            }
        }
    }
}
