using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDamage : MonoBehaviour
{
    [SerializeField] private YarnAttributesSO yarnBallAttributes;
    private Rigidbody rb;
    private float minMass;

    private void Awake() {
        rb = gameObject.GetComponent<Rigidbody>(); // Assumes it exists because it does for all yarn balls
        minMass = yarnBallAttributes.mass;
    }

    public void Damage() {
        rb.mass *= yarnBallAttributes.damageMod;
        if (rb.mass < minMass) Destroy(gameObject);
        else transform.localScale *= yarnBallAttributes.damageMod;
    }
}
