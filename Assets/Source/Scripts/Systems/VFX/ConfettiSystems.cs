using Kuhpik;
using System.Collections;
using UnityEngine;

public class ConfettiSystems : GameSystem, IIniting
{
    [SerializeField] private ParticleSystem[] confetti;

    [SerializeField] private float timeNextScreenStart = 1.5f;
    public void OnInit()
    {
        if (game.isVictory)
        {
            StartCoroutine(offsetTimeStartConfetti());
            game.characters[0].animator.SetBool("Dance", true);
        }
        else
            Bootstrap.ChangeGameState(EGamestate.RateUs);
    }

    private IEnumerator offsetTimeStartConfetti()
    {
        yield return new WaitForSeconds(0.5f);
            for (int b = 0; b < confetti.Length; b++)
            {
                confetti[b].Play();
            }
            StartCoroutine(FinisshUIStart());
    }
    private IEnumerator FinisshUIStart()
    {
       yield return new WaitForSeconds(timeNextScreenStart);
        Bootstrap.ChangeGameState(EGamestate.RateUs);
    }
}
