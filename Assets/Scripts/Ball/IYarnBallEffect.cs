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
    private Transform ball1;
    private Transform ball2;

    public StickyEffect(Transform ball1, Transform ball2)
    {
        this.ball1 = ball1;
        this.ball2 = ball2;
    }

    public void CreateEffect()
    {
        // Initialize any necessary setup before applying the effect
    }

    public void ApplyEffect()
    {
        Debug.Log("Applying Sticky Effect");

        Rigidbody rb1 = ball1.GetComponent<Rigidbody>();
        Rigidbody rb2 = ball2.GetComponent<Rigidbody>();

        if (rb1 != null && rb2 != null)
        {
            FixedJoint joint = ball1.gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = rb2;
            joint.breakForce = float.PositiveInfinity;
        }
    }
}
