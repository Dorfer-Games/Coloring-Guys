using UnityEngine;

using System;
using Kuhpik;
using System.Collections;

public class RateUsSystem : GameSystem, IIniting
{
    private RateUsComponent RateUsComponent;
    private int Ocenka = 0;
    [Header("Settings First Start RateUs")]
    [Tooltip("Время указывается в секундах")]
    [SerializeField] private static float FirstTimeRateUsStart = 300f;
    [SerializeField] private static bool timeFirstStart;
    [Header("Время(в часах), через которое RateUs будет показано вновь после закрытия")]
    [SerializeField] private int HourEnabledRateUs = 24;
    [Header("Ссылка для отправки оценки")]
    [SerializeField] private string url;

    void IIniting.OnInit()
    {

        RateUsComponent = GameObject.FindObjectOfType<FinishUIScreen>().RateUsUI;
        ChangeDataRateUs();
        SetStarRateUs();
    }

    void Start()
    {
        InitTimeStartRateUs();
    }


    void InitTimeStartRateUs()
    {
        if (FirstTimeRateUsStart > 0f)
        {
            if (!timeFirstStart)
            {
                StartCoroutine(FirstTimeRateUs());
                timeFirstStart = true;
            }
            else
            {
                StartCoroutine(FirstTimeRateUs());
            }
        }
    }

    private void ChangeDataRateUs()
    {
        if (player.RateUs > -1)
        {
            if (player.RateUs == 0)
            {
                if (game.isVictory && FirstTimeRateUsStart <= 0f)
                {
                    RateUsComponent.RateUs.SetActive(true);
                }
            }
            if (player.RateUs > 0)
            {
                if (game.isVictory)
                {
                    if (ChangeDateTimeRateUs())
                    {
                        RateUsComponent.RateUs.SetActive(true);
                    }
                }
            }
        }
    }

    private void SetStarRateUs()
    {
        /*var Ocenka = 1;
        for (int b = 0; b < RateUsComponent.Star.Length; b++)
        {
            RateUsComponent.Star[b].onClick.AddListener(delegate { SelectedRateUs(Ocenka); });
            Ocenka++;
        }*/
        RateUsComponent.SendOcenkaGame.onClick.AddListener(delegate { SendRateUs(); });
        RateUsComponent.CloseRateUs.onClick.AddListener(delegate { CloseRateUs(); });
    }

    private void CloseRateUs()
    {
        RateUsComponent.RateUs.SetActive(false);
        if (player.RateUs > -1 && player.RateUs < 3)
        {
            player.RateUs++;
            SetDateRateUs();
        }
        else if (player.RateUs > -1 && player.RateUs >= 2) player.RateUs = -1;
    }



    private void SendRateUs()
    {
        if (Ocenka > 0 && Ocenka < 5)
        {
            RateUsComponent.RateUs.SetActive(false);
            player.RateUs = -1;
        }
        else if(Ocenka > 0 && Ocenka >= 5)
        {
            RateUsComponent.RateUs.SetActive(false);
            player.RateUs = -1;
            Application.OpenURL(url);
        }
    }


    public void SelectedRateUs(int ocenka)
    {
        Ocenka = ocenka;
        RateUsComponent.SendOcenkaGame.gameObject.SetActive(true);
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
        if (DateTime.Now >= DateTime.Parse(player.RateUsDateTime))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private IEnumerator FirstTimeRateUs()
    {
        while (FirstTimeRateUsStart > 0f) {
                FirstTimeRateUsStart -= Time.deltaTime;
            yield return null;
        }
    }
}
