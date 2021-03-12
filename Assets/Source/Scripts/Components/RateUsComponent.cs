using UnityEngine;

using UnityEngine.UI;
using System;
[Serializable]

public class Star {
    public GameObject StarActive, StarNotActive;
}
public class RateUsComponent : MonoBehaviour
{
    public GameObject RateUs;

    public Star[] Star;
    public Button SendOcenkaGame, CloseRateUs;

    public void ActivateStar(int index)
    {
        Star[index].StarActive.SetActive(true);
        Star[index].StarNotActive.SetActive(false);
    }
    public void DiactvateStar(int index)
    {
        Star[index].StarActive.SetActive(false);
        Star[index].StarNotActive.SetActive(true);
    }
}
