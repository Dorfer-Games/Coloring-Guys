using UnityEngine;
using Kuhpik;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class AudioSysytem : GameSystemWithScreen<GameUIScreen>, IIniting
{
    public static AudioSysytem audioSysytem { get; private set; }
    
    [SerializeField] private AudioClip victory, stackCollect, dead, defeat, spawn_Stack, startAudio, collisionAudio;
    private AudioSource audio;


    private void Awake()
    {
        if (audioSysytem == null) audioSysytem = this;
        audio = GetComponent<AudioSource>();
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

    public void AudioCollision()
    {
        audio.PlayOneShot(collisionAudio);
    }
    #endregion
}
