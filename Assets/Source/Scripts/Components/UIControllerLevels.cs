using UnityEngine;
using Kuhpik;


public class UIControllerLevels : GameSystem, IIniting
{
    [SerializeField] private UILevelsProgressBar[] levelsProgressBars;
    [SerializeField] private GameObject levelsProgressBarUI, parentScreen;


    private int number_Level = 0;

    void Start()
    {
        LevelLoadingSystem.loadingSystem.OnLevel += (x) => {
            number_Level = LevelLoadingSystem.loadingSystem.levelAmount;
        };
        levelsProgressBars = new UILevelsProgressBar[LevelLoadingSystem.loadingSystem.countLevelsFirstIteration + 1];
    }

    void IIniting.OnInit()
    {
        CreateLevelsUI();
        var maxLevel = number_Level;

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
        var amountLevels = player.lastIterationLevels;
        int levelUI = LevelLoadingSystem.loadingSystem.levelUIProgressBar;
        for (int d = 0; d < levelsProgressBars.Length; d++)
        {
            levelsProgressBars[d].SetText(levelUI + d + 1);
        }
    }


    private void CreateLevelsUI()
    {
        var countLevelFirstIteratin = LevelLoadingSystem.loadingSystem.countLevelsFirstIteration;
        for (int d = 0; d <= countLevelFirstIteratin; d++)
        {
            UILevelsProgressBar progressBar = Instantiate(levelsProgressBarUI, parentScreen.transform).GetComponent<UILevelsProgressBar>();
            levelsProgressBars[d] = progressBar;
        }
    }
}
