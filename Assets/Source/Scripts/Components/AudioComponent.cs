using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AudioComponent : MonoBehaviour
{
    private AudioSource audio;

    private bool playAudio, isPlayer;
    private void Start()
    {
            audio = GetComponent<AudioSource>();
        if (transform.name == "Player")
        {
            audio.enabled = true;
            StartAudio();
            isPlayer = true;
        }
    }


    public void EnabledAudio()
    {
        if (isPlayer) {
            audio.enabled = true;
            if(!playAudio)
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
