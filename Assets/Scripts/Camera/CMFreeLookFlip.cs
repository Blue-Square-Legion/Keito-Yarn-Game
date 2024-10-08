using Cinemachine;
using UnityEngine;

public class CMFreeLookFlip : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;
    private Transform originalLookAt;
    private bool isFlipped = false;

    void Start()
    {
        if (freeLookCamera != null)
        {
            originalLookAt = freeLookCamera.m_LookAt;
        }
    }

    void Update()
    {
        if (freeLookCamera != null && originalLookAt != null)
        {
            UpdateCameraDirection();
        }

        HandleFreeLookInput();
    }

    void FlipCamera()
    {
        isFlipped = !isFlipped;
        if (isFlipped)
        {
            freeLookCamera.m_LookAt = null;
        }
        else
        {
            freeLookCamera.m_LookAt = originalLookAt;
        }
    }

    void UpdateCameraDirection()
    {
        Vector3 currentPosition = freeLookCamera.transform.position;
        Vector3 directionToLookAt = originalLookAt.position - currentPosition;
        Vector3 targetDirection = isFlipped ? -directionToLookAt : directionToLookAt;

        float currentXRotation = freeLookCamera.transform.eulerAngles.x;

        Quaternion newRotation = Quaternion.LookRotation(targetDirection);
        freeLookCamera.transform.rotation = Quaternion.Euler(currentXRotation, newRotation.eulerAngles.y, newRotation.eulerAngles.z);
    }

    void HandleFreeLookInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (!isFlipped)
        {
            horizontalInput = -horizontalInput;
            verticalInput = -verticalInput;
        }

        freeLookCamera.m_XAxis.Value += horizontalInput;
        freeLookCamera.m_YAxis.Value += verticalInput;
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
        FlipCamera();
    }
}
