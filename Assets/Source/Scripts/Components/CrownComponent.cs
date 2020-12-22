using UnityEngine;

using DG.Tweening;

public class CrownComponent : MonoBehaviour
{
    [SerializeField] private GameObject crown;

    [Header("Settings Move Crown")]
    [SerializeField] private float speedMove;

    private LeaderboardSystem leaderboard;


    private void Start()
    {
        leaderboard = GameObject.FindObjectOfType<LeaderboardSystem>();
        leaderboard.OnCrownEnter += ChangeName;
        //MoveCrown();
    }


    private void MoveCrown()
    {
        var seq = DOTween.Sequence();
            seq.Append(crown.transform.DOLocalMoveY(2.42f, speedMove));
            seq.AppendInterval(0.3f);
            seq.Append(crown.transform.DOLocalMoveY(2.742f, speedMove));
        seq.Play();
        
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
