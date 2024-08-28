using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MangetEffect", menuName = "Effects/MagnetEffect")]
public class MagnetSO : YarnBallEffectSO
{
    private RaycastHit[] hits;
    [SerializeField] private float _magballRadius;
    [SerializeField] private float _pullForce, _pushForce;
    GameObject _ball;


    public override void CreateEffect(GameObject ball, Transform target = null)
    {
        _ball = ball;
    }
    public override void ApplyEffect(GameObject ball, Rigidbody ballRigidbody, Rigidbody targetRigidbody = null)
    {
        hits = Physics.SphereCastAll(ball.transform.position, _magballRadius, Vector3.forward, 1f, 8);
    }
    public override bool ShouldApplyOnCollision()
    {
        return true;
    }

    private void OnDrawGizmoSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(_ball.transform.position, _magballRadius);
    }
}
