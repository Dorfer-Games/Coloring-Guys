using UnityEngine;
using System.Collections;
using System;
using Kuhpik;
using TMPro;

public class GameManager : GameSystemWithScreen<GameUIScreen>, IIniting, IUpdating
{
    public static GameManager gameManager { get; private set; }

    public event Action<bool> StartGame;

    [SerializeField] private TMP_Text numberStartGameText;
    float timeStartGame = 3.5f;


    private void Awake()
    {
     
        if (gameManager == null) gameManager = this;
    }

    void IUpdating.OnUpdate()
    {
        if (numberStartGameText.gameObject.activeSelf)
        {
            if (timeStartGame > 0) {
                timeStartGame -= Time.deltaTime;
                numberStartGameText.text = Convert.ToInt32(timeStartGame).ToString();
            }
            if (timeStartGame <= 0.5f)
            {
                numberStartGameText.text = "GO!";
            }
        }
    }
    void IIniting.OnInit()
    {
        StartGame?.Invoke(false);
        StartCoroutine(TimeStartGame());
    }


    private IEnumerator TimeStartGame()
    {
        yield return new WaitForSeconds(3.5f);
        StartGame?.Invoke(true);
        yield return new WaitForSeconds(1.5f);
        numberStartGameText.gameObject.SetActive(false);
    }
}
