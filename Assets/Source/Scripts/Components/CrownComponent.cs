using UnityEngine;

public class CrownComponent : MonoBehaviour
{
    [SerializeField] private GameObject crown;
    
    private LeaderboardSystem leaderboard;


    private void Start()
    {
        leaderboard = GameObject.FindObjectOfType<LeaderboardSystem>();
        leaderboard.OnCrownEnter += ChangeName;
    }







    private void ChangeName(string name)
    {
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
