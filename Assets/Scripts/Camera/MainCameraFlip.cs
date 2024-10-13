using UnityEngine;
using Cinemachine;
using System.Collections;

public class MainCameraFlip : MonoBehaviour
{
    private Camera mainCamera;
    private bool isFlipped = false;
    private CinemachineBrain cinemachineBrain;
    private bool isFlipping = false;

    void Start()
    {
        mainCamera = Camera.main;

        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
        if (cinemachineBrain == null)
        {
            Debug.LogError("CinemachineBrain not found on the main camera!");
        }
    }

    void Update()
    {
        if (cinemachineBrain != null)
        {
            cinemachineBrain.ManualUpdate();
        }
    }

    private IEnumerator FlipCamera()
    {
        if (isFlipping) yield return null;
        isFlipping = true;

        isFlipped = !isFlipped;

        Vector3 currentRotation = mainCamera.transform.eulerAngles;
        float newYRotation = currentRotation.y + 180f;
        mainCamera.transform.rotation = Quaternion.Euler(currentRotation.x, newYRotation, currentRotation.z);

        isFlipping = false;
        yield return null;
    }

    private void OnEnable()
    {
        InputManager.Input.Player.Enable();

        InputManager.Input.Player.Focus.performed += Focus_performed;
    }

    private void OnDisable()
    {
        InputManager.Input.Player.Disable();

        InputManager.Input.Player.Focus.performed -= Focus_performed;
    }

    private void Focus_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        StartCoroutine(FlipCamera());
    }
}
