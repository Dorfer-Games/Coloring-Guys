using Kuhpik;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DebugCheatingSystem : GameSystem, IIniting
{
    [SerializeField] GameObject[] cheatingPanels;
    [SerializeField] Button nextPageButton;
    
    int page = 0;
    void Start()
    {
        //this.enabled = false;
    }
    void IIniting.OnInit()
    {
        config.Init(config.GameValusConfigs);
        /*#region DEBUG

        for (int i = 0; i < cheatingPanels.Length; i++)
        {
            cheatingPanels[i].SetActive(true);
            cheatingPanels[i].transform.GetComponentsInChildren<CheatSliderComponent>().Any(x => x.Subscribe(config.gameValuesDict[x.Type]));

            if (i != 0)
            {
                cheatingPanels[i].SetActive(false);
            }
        }

        nextPageButton.gameObject.SetActive(true);
        nextPageButton.onClick.AddListener(NextPage);

        #endregion*/
    }

    void NextPage()
    {
        page++;

        if (page == cheatingPanels.Length)
        {
            page = 0;
        }

        for (int i = 0; i < cheatingPanels.Length; i++)
        {
            cheatingPanels[i].SetActive(i == page);
        }
    }
}
