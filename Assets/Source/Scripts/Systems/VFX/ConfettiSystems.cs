using Kuhpik;
using System.Collections;
using UnityEngine;

public class ConfettiSystems : GameSystem, IIniting
{
    [SerializeField] private ParticleSystem[] confetti;
    [SerializeField] private float timeNextScreenStart = 1.5f;

    [Header("Animation")]
    [SerializeField] float yPositionToFixJump;

    public void OnInit()
    {
        if (game.isVictory)
        {
            StartCoroutine(offsetTimeStartConfetti());

            //Quick fix for dancing animation
            var character = game.characters[0].rigidbody.transform;
            var position = character.position;

            game.characters[0].animator.SetBool("Jumping", false);
            position.y = yPositionToFixJump;
            character.position = position;

            game.characters[0].animator.Play("Samba Dancing");
        }

        else Bootstrap.ChangeGameState(EGamestate.RateUs);
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
