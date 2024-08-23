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
    private GameObject _redBall, _otherBall;
    private Rigidbody _otherBallRB;
    private Vector3 _redExtraForce = new Vector3(30f, 0, 30f);
    public ExcessiveForceEffect(GameObject ballHit) 
    {
        _otherBall = ballHit;
        _otherBallRB = _otherBall.GetComponent<Rigidbody>();
    }
    public void CreateEffect()
    {
        
    }

    public void ApplyEffect()
    {
        _otherBallRB.AddForce(_otherBallRB.velocity + _redExtraForce, ForceMode.Impulse);
        Debug.Log("Adding Red Force");
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
