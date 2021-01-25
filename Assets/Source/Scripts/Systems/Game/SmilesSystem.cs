using Kuhpik;

using UnityEngine;

public class SmilesSystem : GameSystemWithScreen<GameUIScreen>
{
    [SerializeField] GameObject leaderboardElementPrefab;



    public void CreateSmiles(Transform other, Transform mainObject, bool isRandom)
    {
        var randomNumber = 0;
        if (isRandom)
        randomNumber = ChangeSpawnSmilesRandom();

        if (randomNumber == 0) {
            if (other != null) {
                var component = Instantiate(leaderboardElementPrefab, screen.Smiles).GetComponent<LookAtSmiles>();
                component.Target = other.transform;
                component.smilesType = 1;
            }
            if (mainObject != null)
            {
                var component_ = Instantiate(leaderboardElementPrefab, screen.Smiles).GetComponent<LookAtSmiles>();
                component_.Target = mainObject.transform;
                component_.smilesType = 0;
            }
        }
    }

    private int ChangeSpawnSmilesRandom() {
        var rand = Random.Range(0,101);
        if (rand >= 70)
        {
            return 0;
        }
        else
        return 1;
}
}
