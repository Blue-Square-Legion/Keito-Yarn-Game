using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    [SerializeField] float _windForce = 2f;
    public Vector3 fanDirection = Vector3.right;



    private void OnTriggerStay(Collider other)
    {
        var hitObj = other.gameObject;
        if (hitObj != null)
        {
           
              
                var rb = hitObj.GetComponent<Rigidbody>();
                rb.AddForce(fanDirection * _windForce);

        }
    }
}