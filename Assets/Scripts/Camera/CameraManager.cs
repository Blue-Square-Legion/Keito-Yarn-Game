using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    public CinemachineFreeLook[] cameras;

    public CinemachineFreeLook cameraA;
    public CinemachineFreeLook cameraB;

    public CinemachineFreeLook startCamera;
    private  CinemachineFreeLook currentCamera;
    private int num = 0;


    private void Start()
    {
        currentCamera = startCamera;

        for(int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i] == currentCamera)
            {
                cameras[i].Priority = 20;
            }
            else
            {
                cameras[i].Priority = 10;
            }
        }
    }

    public void SwitchCamera(CinemachineFreeLook newCam)
    {
        currentCamera = newCam;

        currentCamera.Priority = 20;

        for(int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i] != currentCamera)
            {
                cameras[i].Priority = 10;
            }
        }
    }

    private void Update()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            if(num == cameras.Length-1)
            {
                num = 0;
                SwitchCamera(cameras[num]);
            }
            else
            {
                num++;
                SwitchCamera(cameras[num]);
            }
        }
    }
}
