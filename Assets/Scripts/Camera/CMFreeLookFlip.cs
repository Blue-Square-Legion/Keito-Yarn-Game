using Cinemachine;
using System.Collections;
using UnityEngine;

public class CMFreeLookFlip : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;
    private Transform originalLookAt;
    private bool isFlipped = false;
    private float _cameraSens = 1.0f;
    private float _horizontalSens = 1.5f;
    private float _verticalSens = 0.03f;
    public float cameraSens
    {
        get
        {
            return _cameraSens;
        }
        set
        {
            _cameraSens = value;
            _horizontalSens = 1.5f * _cameraSens;
            _verticalSens = 0.03f * _cameraSens;
        }
    }
    private bool isInputEnabled;
    private bool isFlipping = false;

    void Start()
    {
        if (freeLookCamera != null)
        {
            originalLookAt = freeLookCamera.m_LookAt;
        }
    }

    void Update()
    {
        if (!isInputEnabled) return;

        if (freeLookCamera != null && originalLookAt != null)
        {
            UpdateCameraDirection();
        }

        HandleFreeLookInput();
    }

    private IEnumerator FlipCamera()
    {
        if (isFlipping) yield return null;
        isFlipping = true;

        isFlipped = !isFlipped;
        if (isFlipped)
        {
            freeLookCamera.m_LookAt = null;
        }
        else
        {
            freeLookCamera.m_LookAt = originalLookAt;
        }

        isFlipping = false;
        yield return null;
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

        freeLookCamera.m_XAxis.Value += horizontalInput * _horizontalSens;
        freeLookCamera.m_YAxis.Value += verticalInput * _verticalSens;
    }

    private void OnEnable()
    {
        InputManager.Input.Player.Enable();
        isInputEnabled = true;
        InputManager.Input.Player.Focus.performed += Focus_performed;
    }

    private void OnDisable()
    {
        InputManager.Input.Player.Disable();
        isInputEnabled = false;
        InputManager.Input.Player.Focus.performed -= Focus_performed;
    }

    private void Focus_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        StartCoroutine(FlipCamera());
    }
}
