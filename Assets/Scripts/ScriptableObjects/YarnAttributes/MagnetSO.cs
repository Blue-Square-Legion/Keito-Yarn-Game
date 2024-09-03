using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MangetEffect", menuName = "Effects/MagnetEffect")]
public class MagnetSO : YarnBallEffectSO
{
    private RaycastHit[] hits;
    //private List<GameObject> yarnInRadius;
    [SerializeField] private float _magballRadius;
    [SerializeField] private float _pullForce, _pushForce;
    public LayerMask layerMask;
    private GameObject _ball;

    //ball is the game object the script is attached to. Target is the other ball the game object collides with. Not necesary for this script
    public override void CreateEffect(GameObject ball, Transform target = null)
    {
        Debug.Log("Magnet Field Created");
        _ball = ball;
        MagnetEffect MagEffect = new(_magballRadius, _pullForce, _pullForce);
        MagEffect.StartCoroutine("CreateMagnetField");
        MagEffect.OnDrawGizmos();
    }
    public override void ApplyEffect(GameObject ball, Rigidbody ballRigidbody, Rigidbody targetRigidbody = null)
    {
        Debug.Log("Test");
    } 
    public override bool ShouldApplyOnCollision()
    {
        return false;
    }

}
