using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExcessiveForceEffect", menuName = "Effects/ExcessiveForceEffect")]
public class ExcessiveForceEffectSO : YarnBallEffectSO
{
    [SerializeField] private float forceMultiplier = 5f;
    [SerializeField] private float velocityThreshold = 5f; // Threshold to consider the ball as "moving"

    public override void CreateEffect(GameObject ball, Transform target = null) { }

    public override bool ShouldApplyOnCollision()
    {
        return true;
    }

    public override void ApplyEffect(GameObject ball, Rigidbody ballRigidbody, Rigidbody targetRigidbody = null)
    {
        if (ballRigidbody == null || targetRigidbody == null)
        {
            Debug.LogError("ExcessiveForceEffect cannot be applied: either ball Rigidbody or target Rigidbody is missing.");
            return;
        }

        if (ballRigidbody.velocity.magnitude > velocityThreshold)
        {
            Vector3 multiplicativeForce = ballRigidbody.velocity * forceMultiplier * -1;
            targetRigidbody.AddForce(multiplicativeForce, ForceMode.Impulse);
            Debug.Log($"Applying Multiplicative Force Effect to {ball.name} with multiplier: {forceMultiplier}");
        }
        else
        {
            Debug.Log($"{ball.name} is stationary; no excessive force applied.");
        }
    }
}
