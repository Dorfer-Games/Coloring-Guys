using System;
using System.Collections;
using System.Collections.Generic;
using AudienceNetwork;
using Kuhpik;
using Supyrb;
using UnityEngine;

public class AdvertismentManager : MonoBehaviour
{
    #region Constants

    private const string MaxSdkKey = "6AQkyPv9b4u7yTtMH9PT40gXg00uJOTsmBOf7hDxa_-FnNZvt_qTLnJAiKeb5-2_T8GsI_dGQKKKrtwZTlCzAR";
    private const string InterstitialAdUnitId = "6ef2e5a983b81263";
    private const string RewardedAdUnitId = "6a441a333e9edbb8";
    private const string RewardedInterstitialAdUnitId = "ENTER_REWARD_INTER_AD_UNIT_ID_HERE";
    private const string BannerAdUnitId = "ENTER_BANNER_AD_UNIT_ID_HERE";
    private const string MRecAdUnitId = "ENTER_MREC_AD_UNIT_ID_HERE";

    #endregion

    #region Variables
    
    public struct RewardedData
    {
        public Action callback;
        public string reason;
    }
    
    // Interstitial Settings
    public int minLevelToShowInterstitial = 3;
    [SerializeField]
    private int InterstitialCooldown = 45;
    private int interstitialRetry = 0;
    
    // Rewarded Settings
    private int rewardedRetry = 0;
    private RewardedData rewardedData;
    
    //Helpers
    private float currentTime = 0f;
    private bool noAds = false;
    
    #endregion
    
    
    public static AdvertismentManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(gameObject);
        }
        
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) =>
        {
            Debug.Log("MAX SDK Initialized");

            AdSettings.SetDataProcessingOptions(new string[] { });
            InitializeInterstitialAds();
            InitializeRewardedAds();


            #if DEBUG
            MaxSdk.ShowMediationDebugger();
            #endif
        };

        MaxSdk.SetHasUserConsent(false); //Что бы поставить true надо спрашивать согласие у юзера.
        MaxSdk.SetSdkKey(MaxSdkKey);
        MaxSdk.InitializeSdk();
    }

    public void Init(bool noAds=false)
    {
        this.noAds = noAds;
    }

    public void SetupNoAds(bool value)
    {
        noAds = value;
    }

#region Interstitial ADS


    private void InitializeInterstitialAds()
    {
        // Attach callback
        MaxSdkCallbacks.OnInterstitialLoadedEvent += OnInterstitialLoadedEvent;
        MaxSdkCallbacks.OnInterstitialLoadFailedEvent += OnInterstitialFailedEvent;
        MaxSdkCallbacks.OnInterstitialAdFailedToDisplayEvent += InterstitialFailedToDisplayEvent;
        MaxSdkCallbacks.OnInterstitialHiddenEvent += OnInterstitialDismissedEvent;
        MaxSdkCallbacks.OnInterstitialDisplayedEvent += OnInterstitialDisplayEvent;
        

        // Load the first interstitial
        LoadInterstitial();
    }

    private void LoadInterstitial()
    {
        MaxSdk.LoadInterstitial(InterstitialAdUnitId);
    }
    
    public void ShowInterstitial()
    {
        if (IsInterstitialReady())
        {
            var available = MaxSdk.IsInterstitialReady(InterstitialAdUnitId);
            OnADClicked("interstitial", "on_level_start", available ? "success" : "not_available");

            if (available)
            {
                OnADStarted("interstitial", "on_level_start", "start");
                MaxSdk.ShowInterstitial(InterstitialAdUnitId);
            }
            else
            {
                Signals.Get<OnInterstitialAdClosed>()?.Dispatch();
                Bootstrap.GameRestart(0); //Small hack
                LoadInterstitial();
            }
        }
    }

    private void OnInterstitialDisplayEvent(string adUnitId)
    {
        OnADWatched("interstitial", "on_level_start", "watched");
        Bootstrap.GameRestart(0); //Small hack cause no other interstitials reasons expect on game end.
    }

    private void OnInterstitialLoadedEvent(string adUnitId)
    {
        // Interstitial ad is ready to be shown. MaxSdk.IsInterstitialReady(adUnitId) will now return 'true'

        // Reset retry attempt
        interstitialRetry = 0;
    }

    private void OnInterstitialFailedEvent(string adUnitId, int errorCode)
    {
        // Interstitial ad failed to load 
        // We recommend retrying with exponentially higher delays up to a maximum delay (in this case 64 seconds)

        interstitialRetry++;
        double retryDelay = Math.Pow(2, Math.Min(6, interstitialRetry));
    
        Invoke("LoadInterstitial", (float) retryDelay);
    }

    private void InterstitialFailedToDisplayEvent(string adUnitId, int errorCode)
    {
        //OnADCanceled("interstitial","on_level_start",$"failed_to_display_{errorCode}");
        // Interstitial ad failed to display. We recommend loading the next ad
        LoadInterstitial();
    }

    private void OnInterstitialDismissedEvent(string adUnitId)
    {
        // Interstitial ad is hidden. Pre-load the next ad
        //OnADCanceled("interstitial",$"{rewardedData.reason}",$"dismissed");
        currentTime  = Time.realtimeSinceStartup;
        LoadInterstitial();
    }
    
    public bool IsInterstitialReady()
    {
        return Time.realtimeSinceStartup-currentTime >= InterstitialCooldown && noAds == false;
    }
  
#endregion

#region Rewarded ADS
    
    public void InitializeRewardedAds()
    {
        // Attach callback
        MaxSdkCallbacks.OnRewardedAdLoadedEvent += OnRewardedAdLoadedEvent;
        MaxSdkCallbacks.OnRewardedAdLoadFailedEvent += OnRewardedAdFailedEvent;
        MaxSdkCallbacks.OnRewardedAdFailedToDisplayEvent += OnRewardedAdFailedToDisplayEvent;
        MaxSdkCallbacks.OnRewardedAdDisplayedEvent += OnRewardedAdDisplayedEvent;
        MaxSdkCallbacks.OnRewardedAdClickedEvent += OnRewardedAdClickedEvent;
        MaxSdkCallbacks.OnRewardedAdHiddenEvent += OnRewardedAdDismissedEvent;
        MaxSdkCallbacks.OnRewardedAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;
    
        // Load the first rewarded ad
        LoadRewardedAd();
    }
    
    private void LoadRewardedAd()
    {
        MaxSdk.LoadRewardedAd(RewardedAdUnitId);
    }
    
    public void ShowRewarded(Action callback, string reason)
    {
        var available = MaxSdk.IsRewardedAdReady(RewardedAdUnitId);
        OnADClicked("rewarded", reason, available ? "success" : "not_available");

        rewardedData = new RewardedData() { callback = callback, reason = reason.ToLower() };
        
        if (available)
        {
            OnADStarted("rewarded", reason, "start");
            MaxSdk.ShowRewardedAd(RewardedAdUnitId);
        }
        else
        {
            LoadRewardedAd();
        }
    }
    
    private void OnRewardedAdLoadedEvent(string adUnitId)
    {
        // Rewarded ad is ready to be shown. MaxSdk.IsRewardedAdReady(adUnitId) will now return 'true'
    
        // Reset retry attempt
        rewardedRetry = 0;
    }
    
    private void OnRewardedAdFailedEvent(string adUnitId, int errorCode)
    {
        // Rewarded ad failed to load 
        // We recommend retrying with exponentially higher delays up to a maximum delay (in this case 64 seconds)
    
        rewardedRetry++;
        double retryDelay = Math.Pow(2, Math.Min(6, rewardedRetry));
        
        Invoke("LoadRewardedAd", (float) retryDelay);
    }
    
    private void OnRewardedAdFailedToDisplayEvent(string adUnitId, int errorCode)
    {
        //OnADCanceled("rewarded",$"{rewardedData.reason}",$"failed_to_display_{errorCode}");
        // Rewarded ad failed to display. We recommend loading the next ad
        LoadRewardedAd();
    }

    private void OnRewardedAdDisplayedEvent(string adUnitId)
    {
    }
    
    private void OnRewardedAdClickedEvent(string adUnitId) {}
    
    private void OnRewardedAdDismissedEvent(string adUnitId)
    {
        // Rewarded ad is hidden. Pre-load the next ad
        //OnADWatched("rewarded",$"{rewardedData.reason}",$"canceled");
        currentTime  = Time.realtimeSinceStartup;
        LoadRewardedAd();
    }
    
    private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward)
    {
        // Rewarded ad was displayed and user should receive the reward
        OnADWatched("rewarded",$"{rewardedData.reason}","watched");
        rewardedData.callback?.Invoke();
    }

#endregion

#region AppMetrica Events by Kuhpik

    void OnADClicked(string type, string reason, string restult)
        {
            var @params = new Dictionary<string, object>() 
            { 
                {"ad_type", type},
                {"placement", reason},
                {"result", restult},
                {"connection", GetInternetConnection()}
            };
    
            AppMetrica.Instance.ReportEvent("video_ads_available", @params);
        }
    
        void OnADStarted(string type, string reason, string restult)
        {
            var @params = new Dictionary<string, object>() 
            { 
                {"ad_type", type},
                {"placement", reason},
                {"result", restult},
                {"connection", GetInternetConnection()}
            };
    
            AppMetrica.Instance.ReportEvent("video_ads_started", @params);
        }
    
        void OnADWatched(string type, string reason, string restult)
        {
            var @params = new Dictionary<string, object>() 
            { 
                {"ad_type", type},
                {"placement", reason},
                {"result", restult},
                {"connection", GetInternetConnection()}
            };
    
            AppMetrica.Instance.ReportEvent("video_ads_watch", @params);
        }
    
        public bool GetInternetConnection()
        {
            return Application.internetReachability != NetworkReachability.NotReachable;
        }

#endregion
}
