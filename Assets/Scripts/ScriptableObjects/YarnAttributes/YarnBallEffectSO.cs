using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class YarnBallEffectSO : ScriptableObject
{
    public virtual void CreateEffect(GameObject ball, Transform target = null) { }

    public abstract bool ShouldApplyOnCollision();

    public abstract void ApplyEffect(GameObject ball, Rigidbody ballRigidbody, Rigidbody targetRigidbody = null);
}
