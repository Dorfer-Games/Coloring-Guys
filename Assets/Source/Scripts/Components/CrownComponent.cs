using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CrownComponent : MonoBehaviour
{
    [SerializeField] private GameObject crown;
    
    private LeaderboardSystem leaderboard;
    private List<SendPlayerData> dataAllPlayers = new List<SendPlayerData>();

    private void Awake()
    {
        leaderboard = GameObject.FindObjectOfType<LeaderboardSystem>();
        leaderboard.OnCrownEnter += ChangeName;
        dataAllPlayers = GetComponentsInChildren<SendPlayerData>().ToList();
    }





    public GameObject Crown()
    {
        return dataAllPlayers[GetComponent<DataAllToPlayerComponent>().skin].GetComponent<SendPlayerData>().Crown;
    }

    private void ChangeName(string name)
    {
        if (crown == null)
        {
           crown = Crown();
        }
        if (!crown.activeSelf && name == transform.name)
        {
            crown.SetActive(true);
        }
        else if(crown.activeSelf && name != transform.name)
        {
            crown.SetActive(false);
        }
    }
}
