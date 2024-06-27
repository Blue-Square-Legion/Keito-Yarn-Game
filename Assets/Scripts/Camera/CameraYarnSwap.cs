using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraYarnSwap : MonoBehaviour
{
    [SerializeField] private new CinemachineFreeLook camera;
    [SerializeField] private GameObject focalpoint;

    /* public void OnMouseEnter()
     {
        // if (Input.GetKeyDown(KeyCode.E))
        // {
             camera.Follow = gameObject.transform;
             camera.LookAt = gameObject.transform;
        // }
     }*/
    private void Start()
    {
        camera = FindObjectOfType<CinemachineFreeLook>();
        focalpoint = GameObject.Find("FocalPoint");
    }


    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            camera.Follow = focalpoint.transform;
            camera.LookAt = focalpoint.transform;
            return;
        }

        if (Input.GetMouseButtonDown(1))
        {
            camera.Follow = gameObject.transform;
            camera.LookAt = gameObject.transform;
           
           
            return;
        }
    }

    

    
}
