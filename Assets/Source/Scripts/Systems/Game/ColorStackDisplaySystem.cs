﻿using Kuhpik;
using Kuhpik.Pooling;
using Supyrb;
using System.Collections.Generic;
using UnityEngine;

public class ColorStackDisplaySystem : GameSystem, IIniting
{
    [SerializeField] GameObject stackPrefab;
    [SerializeField] float offset;
    [SerializeField] string stackPointName;

    Dictionary<Character, Transform> characterHexPoints;

    void IIniting.OnInit()
    {
        if (Mathf.RoundToInt(config.GetValue(EGameValue.DisplayHexes)) == 1)
        {
            characterHexPoints = new Dictionary<Character, Transform>();

            for (int i = 0; i < game.characters.Length; i++)
            {
                var point = game.characters[i].rigidbody.transform.Find(stackPointName);
                characterHexPoints.Add(game.characters[i], point);

                for (int j = 0; j < Mathf.RoundToInt(config.GetValue(EGameValue.ColorMax)); j++)
                {
                    var position = Vector3.zero;
                    position.y = j * offset;

                    PoolingSystem.GetComponent(stackPrefab, out MeshRenderer renderer);
                    renderer.transform.SetParent(point);
                    renderer.transform.localPosition = position;
                    renderer.gameObject.SetActive(false);

                    Color(renderer, game.characters[i].color);
                }
            }

            Signals.Get<HexCountChangedSignal>().AddListener(ChangeHexCountDisplaying, 10);
        }
    }

    void Color(MeshRenderer renderer, Color color)
    {
        var block = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(block);
        block.SetVector("_Color", color);
        renderer.SetPropertyBlock(block);
    }

    void ChangeHexCountDisplaying(Character character, int index)
    {
        for (int i = 0; i < Mathf.RoundToInt(config.GetValue(EGameValue.ColorMax)); i++)
        {
            characterHexPoints[character].GetChild(i).gameObject.SetActive(i < character.stacks);
        }
    }
}
