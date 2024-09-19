using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StickyEffect", menuName = "Effects/StickyEffect")]
public class StickyEffectSO : YarnBallEffectSO
{
    [SerializeField] private float jointBreakForce = 1000.0f;
    [SerializeField] private float massReductionFactor = 0.5f;

    private Dictionary<Rigidbody, float> originalMasses = new Dictionary<Rigidbody, float>();

    public override void CreateEffect(GameObject ball, Transform target = null) { }

    public override bool ShouldApplyOnCollision()
    {
        return true;
    }

    public override void ApplyEffect(GameObject ball, Rigidbody ballRigidbody, Rigidbody targetRigidbody)
    {
        if (ballRigidbody != null && targetRigidbody != null)
        {
            if (!originalMasses.ContainsKey(targetRigidbody))
            {
                originalMasses[targetRigidbody] = targetRigidbody.mass;
                targetRigidbody.mass *= massReductionFactor;
            }

            FixedJoint joint = ball.AddComponent<FixedJoint>();
            joint.connectedBody = targetRigidbody;
            joint.breakForce = jointBreakForce;
            joint.breakTorque = jointBreakForce;

            Debug.Log("Applying Sticky Effect to " + ball.name);

            ballRigidbody.gameObject.AddComponent<JointBreakListener>().Initialize(this, targetRigidbody);
        }
        else
        {
            Debug.LogError("Sticky effect cannot be applied: either ballRigidbody or targetRigidbody is missing.");
        }
    }

    public void RestoreOriginalMass(Rigidbody targetRigidbody)
    {
        if (originalMasses.ContainsKey(targetRigidbody))
        {
            targetRigidbody.mass = originalMasses[targetRigidbody];
            originalMasses.Remove(targetRigidbody);
            Debug.Log("Restored original mass to " + targetRigidbody.gameObject.name);
        }
    }
}

public class JointBreakListener : MonoBehaviour
{
    private StickyEffectSO stickyEffect;
    private Rigidbody attachedRigidbody;

    public void Initialize(StickyEffectSO effect, Rigidbody rb)
    {
        stickyEffect = effect;
        attachedRigidbody = rb;
    }

    private void OnJointBreak(float breakForce)
    {
        stickyEffect.RestoreOriginalMass(attachedRigidbody);
        Destroy(this);
    }
}