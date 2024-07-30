using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    [SerializeField] float _windForce = 2f;
    [SerializeField] public bool _up, _right,_forward,_left;
    private void OnTriggerStay(Collider other)
    {
        var hitObj = other.gameObject;
        if (hitObj != null)
        {

            if (_up)
            {
                var dir = transform.up;
                var rb = hitObj.GetComponent<Rigidbody>();
                rb.AddForce(dir * _windForce);
            } 
            if(_right)
            {
                var dir = transform.right;
                var rb = hitObj.GetComponent<Rigidbody>();
                rb.AddForce(dir * _windForce);
            }
            if (_forward)
            {
                var dir = transform.right;
                var rb = hitObj.GetComponent<Rigidbody>();
                rb.AddForce(dir * _windForce);
            }
            if (_left)
            {
                var dir = transform.right;
                var rb = hitObj.GetComponent<Rigidbody>();
                rb.AddForce(dir * _windForce);
            }
        }
    }
}