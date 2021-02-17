﻿using Kuhpik;
using Supyrb;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AdsSystem : GameSystem
{
    private FinishUIScreen finishUI;
    private MenuUIScreen menuUI;
    private static int levelStartAds;
    private void Start()
    {
        finishUI = GameObject.FindObjectOfType<FinishUIScreen>();
        menuUI = GameObject.FindObjectOfType<MenuUIScreen>();
        finishUI.AdsRewardedButton.onClick.AddListener(delegate { AdsRewarded_X5(); });
        menuUI.AdsRewardedStackColor.onClick.AddListener(delegate { AdsRewarded_BoosterColorStack(); });
        ChangeLevel();
    }

    private void ChangeLevel()
    {
        levelStartAds++;
    }
    private void AdsRewarded_X5()
    {
        AdvertismentManager.Instance.ShowRewarded(MoneyAdd_AdsRewarded, $"ad_on_X5");
        LevelLoadingSystem.loadingSystem.AddLevel();
    }


    private void AdsRewarded_BoosterColorStack()
    {
        AdvertismentManager.Instance.ShowRewarded(BosterAddStackColor_AdsRewarded, $"ad_on_boosterColorStack");
    }


    public void AdsInterstitialEndLevelGame()
    {
        if(levelStartAds > 2)
        AdvertismentManager.Instance.ShowInterstitial();
    }



    #region Callback срабатывающие после завершения рекламы
    private void MoneyAdd_AdsRewarded()
    {
        MoneyRewardedSystem.rewardedSystem.AnimationStart(500, MoneyRewardedSystem.rewardedSystem.startPoint_X5);
    }


    private void BosterAddStackColor_AdsRewarded()
    {
        game.characters[0].stacks = Mathf.Clamp(game.characters[0].stacks + 30, 0, Mathf.RoundToInt(config.GetValue(EGameValue.ColorMax)));
        Signals.Get<HexCountChangedSignal>().Dispatch(game.characters[0], game.characters[0].stacks);
        Bootstrap.ChangeGameState(EGamestate.Game);
    }
    #endregion
}