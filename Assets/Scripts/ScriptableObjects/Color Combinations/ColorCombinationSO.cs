using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewColorCombination", menuName = "SO/ColorCombination")]
public class ColorCombinationSO : ScriptableObject
{
    public YarnAttributesSO colorA;
    public YarnAttributesSO colorB;
    public YarnAttributesSO newYarnBall;

    public bool AcceptableCombination(YarnAttributesSO colorA, YarnAttributesSO colorB)
    {
        return this.colorA == colorA && this.colorB == colorB || this.colorA == colorB && this.colorB == colorA;
    }
}
