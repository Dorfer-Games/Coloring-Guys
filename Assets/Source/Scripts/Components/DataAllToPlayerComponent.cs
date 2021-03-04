using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataAllToPlayerComponent : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer meshRenderer;

    public Animator AnimatorPlayer;
    public Transform ColorBricks;
    private List<SendPlayerData> dataAllPlayers = new List<SendPlayerData>();
    public int skin;
        
    private void Awake()
    {
        dataAllPlayers = GetComponentsInChildren<SendPlayerData>().ToList();
        foreach (SendPlayerData data in dataAllPlayers)
        {
            data.gameObject.SetActive(false);
        }
       meshRenderer = Skin();
    }
    public void UpdateBodyColor(Color color)
    {
        meshRenderer.materials[1].color = color;
    }

    public Animator Animator()
    {
        return dataAllPlayers[skin].GetComponent<SendPlayerData>().AnimatorPlayer;
    }
    public SkinnedMeshRenderer Skin()
    {
        return dataAllPlayers[skin].GetComponent<SendPlayerData>().meshRenderer;
    }
    public Transform BricksColorStack()
    {
        return dataAllPlayers[skin].GetComponent<SendPlayerData>().ColorBricks;
    }
    public void EnabledSkinPlayer()
    {
        for (int b = 0; b < dataAllPlayers.Count; b++) {
            if(b == skin)
            dataAllPlayers[skin].gameObject.SetActive(true);
            else dataAllPlayers[b].gameObject.SetActive(false);
        }
    }
}
