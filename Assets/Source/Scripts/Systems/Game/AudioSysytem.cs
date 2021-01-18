using UnityEngine;
using Kuhpik;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class AudioSysytem : GameSystemWithScreen<GameUIScreen>, IIniting
{
    public static AudioSysytem audioSysytem { get; private set; }
    
    [SerializeField] private AudioClip victory, stackCollect, dead, defeat, spawn_Stack, startAudio, collisionAudio;
    public AudioSource audio;


    private void Awake()
    {
        if (audioSysytem == null) audioSysytem = this;
    }


    void IIniting.OnInit()
    {
        GameManager.gameManager.StartGame += (startGame) => { if(!startGame)AudioStartGame(); };
    }
    #region StartAudio

    public void AudioStartGame()
    {
        audio.clip = startAudio;
        audio.Play();
    }
    public void AudioVictory()
    {
        audio.PlayOneShot(victory);
    }

    public void AudioDefeat()
    {
        audio.PlayOneShot(defeat);
    }

    public void AudioDead()
    {
        audio.PlayOneShot(dead);
    }
    public void AudioCollectStack()
    {
        audio.PlayOneShot(stackCollect);;
    }

    public void AudioSpawnStack()
    {
        audio.clip = spawn_Stack;
        audio.Play();
    }

    public void AudioCollision()
    {
        audio.PlayOneShot(collisionAudio);
    }
    #endregion
}
