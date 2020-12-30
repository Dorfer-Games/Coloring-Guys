using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AudioComponent : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    private AudioSource audio;

    private bool playAudio, isPlayer, startGame;
    private void Start()
    {
            audio = GetComponent<AudioSource>();
        if (transform.name == "Player")
        {
            audio.enabled = true;
            isPlayer = true;
        }
    }


    private void Update()
    {
        if (!startGame && isPlayer && !playAudio && !rigidbody.isKinematic)
        {
            EnabledAudio();
            startGame = true;
        }
    }
    public void EnabledAudio()
    {
            if (isPlayer)
            {
            audio.enabled = true;
            if (!playAudio)
                    StartAudio();
        }
    }
    public void DisabledAudio()
    {
        if (isPlayer)
        {
            audio.enabled = false;
            playAudio = false;
        }
    }
    private void StartAudio()
    {
        audio.Play();
        playAudio = true;
    }
}
