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
            game.Player.Remove(@object.gameObject);

            if (character == game.characters[0])
            {
                game.isVictory = false;
                Bootstrap.ChangeGameState(EGamestate.VFX);
                AudioSysytem.audioSysytem.AudioDefeat();
                game.characters[0].audioComponent.DisabledAudio();
            }

            else
            {
                Signals.Get<PlayerNotificationSignal>().Dispatch($"{@object.name} is going down!");
                AudioSysytem.audioSysytem.AudioDead();

                if (game.characters.Count(x => x.isDeath) == game.characters.Length - 1)
                {
                    game.isVictory = true;
                    Bootstrap.ChangeGameState(EGamestate.VFX);
                    AudioSysytem.audioSysytem.AudioVictory();
                    game.characters[0].audioComponent.DisabledAudio();
                }
            }
           // Bootstrap.GetSystem<LiderboardFinishSystem>().AddDeathPlayer(character.rigidbody.gameObject);
            Bootstrap.GetSystem<SmilesSystem>().CreateSmiles(@object, character.onTriggerEnterImpact.lastCollisionPlayer, false);
        }
    }
}
