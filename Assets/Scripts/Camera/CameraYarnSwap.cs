using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraYarnSwap : MonoBehaviour
{
    [SerializeField] private new CinemachineFreeLook camera;
    [SerializeField] private GameObject focalpoint;
    [SerializeField] private GameObject objects;

     public void OnMouseEnter()
     {
        if (Input.GetMouseButtonDown(1))
        {
            objects.transform.parent = this.transform;

            camera.Follow = objects.transform;
            camera.LookAt = objects.transform;

            return;
        }
    }
    private void Start()
    {
        camera = FindObjectOfType<CinemachineFreeLook>();
        focalpoint = GameObject.Find("FocalPoint");
        objects = GameObject.Find("Point");

        

    }


    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            camera.Follow = focalpoint.transform;
            camera.LookAt = focalpoint.transform;
            return;
        }
        OnMouseEnter();
        
        /*
        if (Input.GetKeyDown(KeyCode.E))
        {
            camera.Follow = objects.transform;
            camera.LookAt = objects.transform;
            return;
        }*/

    }

    

    
}
