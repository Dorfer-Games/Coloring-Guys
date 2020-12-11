using Kuhpik;
using UnityEngine;

public class CharacterSizeChangingSystem : GameSystem, IIniting
{
    Transform cameraPoint;

    void IIniting.OnInit()
    {
        var scaleCamera = Mathf.RoundToInt(config.GetValue(EGameValue.ScaleCamera)) == 1f;

        if (!scaleCamera)
        {
            cameraPoint = game.characters[0].rigidbody.transform.Find("Camera point");
            cameraPoint.SetParent(null);
        }

        foreach (var character in game.characters)
        {
            character.rigidbody.transform.localScale = Vector3.one * config.GetValue(EGameValue.CharacterSize);
        }

        if (!scaleCamera)
        {
            cameraPoint.SetParent(game.characters[0].rigidbody.transform);
        }
    }
}
