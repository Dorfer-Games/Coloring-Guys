using Kuhpik;

using UnityEngine;

public class SmilesSystem : GameSystemWithScreen<GameUIScreen>
{
    [SerializeField] GameObject leaderboardElementPrefab;



    public void CreateSmiles(Transform other, Transform mainObject)
    {
        var component = Instantiate(leaderboardElementPrefab, screen.Smiles).GetComponent<LookAtSmiles>();
        component.Target = other.transform;
        component.smilesType = 1;

        var component_ = Instantiate(leaderboardElementPrefab, screen.Smiles).GetComponent<LookAtSmiles>();
        component_.Target = mainObject.transform;
        component_.smilesType = 0;
    }
}
