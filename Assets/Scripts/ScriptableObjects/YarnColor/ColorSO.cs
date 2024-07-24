using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Color", menuName = "SO/Color")]
public class ColorSO : ScriptableObject
{
    public enum BallColor
    {
        Default,
        Red,
        Green,
        Blue,
        Black,
        Brown,
        Grey,
        Pink,
        Purple,
        Sky_Blue,
        White,
        Yellow
    }

    public BallColor Name;
    public Color Color;
    // TODO: Remove prefab in favor of material (easier to change and relies on a single prefab)
    public GameObject YarnPrefab;
    public Material YarnMaterial;
}
