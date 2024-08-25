using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StickyEffect", menuName = "Effects/StickyEffect")]
public class StickyEffectSO : YarnBallEffectSO
{
    [SerializeField] private float jointBreakForce = 1000.0f;
    private Transform _stickyTarget;

    public override void CreateEffect(GameObject ball, Transform target = null) { }

    public override bool ShouldApplyOnCollision()
    {
        return true;
    }

    public override void ApplyEffect(GameObject ball, Rigidbody ballRigidbody, Rigidbody targetRigidbody)
    {
        if (ballRigidbody != null && targetRigidbody != null)
        {
            FixedJoint joint = ball.AddComponent<FixedJoint>();
            joint.connectedBody = targetRigidbody;
            joint.breakForce = jointBreakForce;
            Debug.Log("Applying Sticky Effect to " + ball.name);
        }
        else
        {
            Debug.LogError("Sticky effect cannot be applied: either ballRigidbody or targetRigidbody is missing.");
        }
    }
}
