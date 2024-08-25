using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "YarnAttribute", menuName = "SO/YarnAttribute")]
public class YarnAttributesSO : ScriptableObject
{
    public ColorSO color;
    public float bounciness = 0.98f;
    [Range(0.01f, 10f)] public float mass = 1f;

    [Space(5)]
    public float damageMod = 0.5f;
    public float repairMod = 1f;

    [Space(5)]
    [Range(0.01f, 10f)] public float massMultiplier = 1f;
    public float massCap = 5f;

    [Space(5)]
    public float scaleMultiplier = 1f;
    public float scaleCap = 5f;

    [Space(5)]
    public bool allowDamageCombine = false;

    [Space(5), Tooltip("Balls that this one can merge with")]
    public List<YarnAttributesSO> mergableBalls = new();

    [Space(5), Tooltip("What new colors the yarn ball can merge into if it was hit by the compatible yarn ball.")]
    public List<ColorCombinationSO> acceptableCombinations = new();

     [Space(5), Tooltip("Effects to apply on collision")]
    public List<YarnBallEffectSO> collisionEffects = new();

    [Space(5), Tooltip("General effects that could apply on update or other triggers")]
    public List<YarnBallEffectSO> generalEffects = new();
}
