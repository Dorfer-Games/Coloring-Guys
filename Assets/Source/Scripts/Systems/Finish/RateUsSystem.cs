using Kuhpik;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateUsSystem : GameSystem, IIniting, IDisposing
{
    private const string timerSaveKey = "rateustimerfirstshow";
    private RateUsComponent RateUsComponent;
    private int Ocenka = 0;

    [Header("Settings First Start RateUs")]
    [SerializeField] private float timeFirstRateUs = 0f;

    [Tooltip("Время указывается в секундах")]
    [SerializeField] private static float FirstTimeRateUsStart = 0f;
    [SerializeField] private static bool timeFirstStart;

    [Header("Время(в часах), через которое RateUs будет показано вновь после закрытия")]
    [SerializeField] private int HourEnabledRateUs = 24;

    [Header("Ссылка для отправки оценки")]
    [SerializeField] private string url;

    [Header("Цвет для окраса спрайтов оценки")]
    [SerializeField] private Color colorItem;

    void IIniting.OnInit()
    {
        RateUsComponent = GameObject.FindObjectOfType<RateUs_UI>().RateUsUI;
        ChangeDataRateUs();
        SetStarRateUs();
    }

    //Сохраняем при перезагрузке уровня.
    void IDisposing.OnDispose()
    {
        PlayerPrefs.SetInt(timerSaveKey, Mathf.RoundToInt(FirstTimeRateUsStart));
    }

    void Start()
    {
        FirstTimeRateUsStart = PlayerPrefs.GetInt(timerSaveKey, Mathf.RoundToInt(timeFirstRateUs));
        InitTimeStartRateUs();
    }

    void InitTimeStartRateUs()
    {
        if (FirstTimeRateUsStart > 0f)
        {
            StartCoroutine(FirstTimeRateUs());
        }
    }

    private void ChangeDataRateUs()
    {
        if (game.isVictory)
        {
            if (player.RateUs > -1)
            {
                if (player.RateUs == 0)
                {
                    if (FirstTimeRateUsStart <= 0f)
                    {
                        SendAppMetrica(7);
                    }

                    else Bootstrap.ChangeGameState(EGamestate.Finish);
                }

                if (player.RateUs > 0)
                {
                    if (ChangeDateTimeRateUs())
                    {
                        SendAppMetrica(7);
                    }

                    else
                    {
                        Bootstrap.ChangeGameState(EGamestate.Finish);
                    }
                }
            }
        }
        if (!game.isVictory || player.RateUs <= -1) Bootstrap.ChangeGameState(EGamestate.Finish);
    }

    private void SetStarRateUs()
    {
        RateUsComponent.SendOcenkaGame.onClick.AddListener(delegate { SendRateUs(); });
        RateUsComponent.CloseRateUs.onClick.AddListener(delegate { CloseRateUs(); });
    }

    private void CloseRateUs()
    {
        if (player.RateUs > -1 && player.RateUs < 3)
        {
            player.RateUs++;
            SetDateRateUs();
        }

        else if (player.RateUs > -1 && player.RateUs >= 2) player.RateUs = -1;
        Bootstrap.ChangeGameState(EGamestate.Finish);
        SendAppMetrica(0);
    }

    private void SendRateUs()
    {
        if (Ocenka > 0 && Ocenka < 5)
        {
            player.RateUs = -1;
            Bootstrap.ChangeGameState(EGamestate.Finish);
        }

        else if (Ocenka > 0 && Ocenka >= 5)
        {
            player.RateUs = -1;
            Application.OpenURL(url);
            Bootstrap.ChangeGameState(EGamestate.Finish);
        }

        SendAppMetrica(Ocenka);
    }

    public void SelectedRateUs(int ocenka)
    {
        Ocenka = ocenka;
        SetColorItemOcenka();
        RateUsComponent.SendOcenkaGame.gameObject.SetActive(true);
    }

    private void SetColorItemOcenka()
    {
        for (int b = 0; b < RateUsComponent.Star.Length; b++)
        {
            if (Ocenka > b) RateUsComponent.ActivateStar(b);
            else RateUsComponent.DiactvateStar(b);
        }
    }

    private void SetDateRateUs() // Указываем время для следкющего запуска RateUs
    {
        DateTime dateTime = DateTime.Now;
        dateTime = dateTime.AddHours(HourEnabledRateUs);
        Debug.Log(dateTime);
        player.RateUsDateTime = dateTime.ToString();
    }

    private bool ChangeDateTimeRateUs() // Проверяет прошло нужное количество времени
    {
        return DateTime.Now >= DateTime.Parse(player.RateUsDateTime);
    }

    private IEnumerator FirstTimeRateUs()
    {
        while (FirstTimeRateUsStart > 0f)
        {
            FirstTimeRateUsStart -= Time.deltaTime;
            yield return null;
        }
    }

    private void SendAppMetrica(int type)
    {
        var @params = new Dictionary<string, object>()
        {
            { "type", type}
        };

        if (type == 7) AppMetrica.Instance.ReportEvent("RateUs_passed_required_number_time", @params);
        if (type == 0) AppMetrica.Instance.ReportEvent("RateUs_window_closed", @params);
        if (type >= 1 && type <= 5) AppMetrica.Instance.ReportEvent("RateUs_estimation", @params);

        AppMetrica.Instance.SendEventsBuffer();
    }
}
