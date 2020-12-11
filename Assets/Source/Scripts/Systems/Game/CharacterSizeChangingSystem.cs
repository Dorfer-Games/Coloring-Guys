using Kuhpik;
using UnityEngine;

public class CharacterSizeChangingSystem : GameSystem, IIniting
{
    Transform cameraPoint;

    void IIniting.OnInit()
    {
        if (!config.ScaleCamera)
        {
            cameraPoint = game.characters[0].rigidbody.transform.Find("Camera point");
            cameraPoint.SetParent(null);
        }

        foreach (var character in game.characters)
        {
            character.rigidbody.transform.localScale = Vector3.one * config.CharacterSize;
        }

        if (!config.ScaleCamera)
        {
            cameraPoint.SetParent(game.characters[0].rigidbody.transform);
        }
    }
}
