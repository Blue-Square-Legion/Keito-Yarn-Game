using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IYarnBallEffect
{
    void CreateEffect();
    void ApplyEffect();
}

public class ExcessiveForceEffect : IYarnBallEffect
{
    public void CreateEffect()
    {
        
    }

    public void ApplyEffect()
    {
        Debug.Log("Applying Excessive Force Effect");
    }
}

public class StickyEffect : IYarnBallEffect
{
    private Transform stickyBall;
    private Transform otherBall;

    public StickyEffect(Transform stickyBall, Transform otherBall)
    {
        this.stickyBall = stickyBall;
        this.otherBall = otherBall;
    }

    public void CreateEffect()
    {
        // Initialize any necessary setup before applying the effect
    }

    public void ApplyEffect()
    {
        Debug.Log("Applying Sticky Effect");

        Rigidbody rb1 = stickyBall.GetComponent<Rigidbody>();
        Rigidbody rb2 = otherBall.GetComponent<Rigidbody>();

        if (rb1 != null && rb2 != null)
        {
            FixedJoint joint = stickyBall.gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = rb2;
            joint.breakForce = 1000.0f;
        }
    }
}
