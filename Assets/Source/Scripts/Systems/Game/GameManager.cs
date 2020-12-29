using UnityEngine;
using System.Collections;
using System;
using Kuhpik;
public class GameManager : GameSystemWithScreen<GameUIScreen>, IIniting
{
    public static GameManager gameManager { get; private set; }

    public event Action<bool> StartGame;


    private void Awake()
    {
     
        if (gameManager == null) gameManager = this;
    }


    void IIniting.OnInit()
    {
        StartGame?.Invoke(false);
        StartCoroutine(timeStartGame());
    }


    private IEnumerator timeStartGame()
    {
        yield return new WaitForSeconds(2.5f);
        StartGame?.Invoke(true);
    }
}
