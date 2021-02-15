using Kuhpik;
using System.Collections;
using UnityEngine;

public class ConfettiSystems : GameSystem, IIniting
{
    [SerializeField] private ParticleSystem[] confetti;

    [SerializeField] private float timeNextScreenStart = 1.5f;
    public void OnInit()
    {
        if (game.isVictory) {
            for (int b = 0; b < confetti.Length;b++) {
                confetti[b].Play();
            }
            StartCoroutine(FinisshUIStart());
        }
        else
        {
            Bootstrap.ChangeGameState(EGamestate.Finish);
        }
    }

    private IEnumerator FinisshUIStart()
    {
       yield return new WaitForSeconds(timeNextScreenStart);
        Bootstrap.ChangeGameState(EGamestate.Finish);
    }
}
