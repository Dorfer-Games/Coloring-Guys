using UnityEngine;
using Kuhpik;


public class UIControllerLevels : GameSystem, IIniting
{
    [SerializeField] private UILevelsProgressBar[] levelsProgressBars;


    private int number_Level = 0;

    void Start()
    {
        LevelLoadingSystem.loadingSystem.OnLevel += (x) => {
            number_Level = x;
        };
    }

    void IIniting.OnInit()
    {
        
        var maxLevel = config.GetValue(EGameValue.LevelsCount);

        for (int d = 0; d < levelsProgressBars.Length; d++)
        {
            levelsProgressBars[d].NotPassed();
            if (d <= maxLevel)
            {
                levelsProgressBars[d].Passed();
            }
            if (number_Level == d)
            {
                levelsProgressBars[d].Process();
            }
        }
    }
}
