using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AudioSysytem : MonoBehaviour
{
    public static AudioSysytem audioSysytem { get; private set; }

    private AudioSource audio;
    [SerializeField] private AudioClip victory, stackCollect, dead, defeat, spawn_Stack;


    private void Start()
    {
        if (audioSysytem == null) audioSysytem = this;
        audio = GetComponent<AudioSource>();
    }

    #region StartAudio
    public void AudioVictory()
    {
        audio.clip = victory;
        audio.Play();
    }

    public void AudioDefeat()
    {
        audio.clip = defeat;
        audio.Play();
    }

    public void AudioDead()
    {
        audio.clip = dead;
        audio.Play();
    }
    public void AudioCollectStack()
    {
        audio.clip = stackCollect;
        audio.Play();
    }

    public void AudioSpawnStack()
    {
        audio.clip = spawn_Stack;
        audio.Play();
    }
    #endregion
}
