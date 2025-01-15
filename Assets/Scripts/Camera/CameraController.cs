using UnityEngine;

// This is the main script for controlling the camera
// TODO: Make this work with the new input system.
public class CameraController : MonoBehaviour
{
    public float sensitivity = 2f; // Mouse sensitivity
    public float moveSpeed = 10f; // Speed of movement with keys
    [SerializeField] private float maxDistance = 7f;

    private float xRotation = 0f; // Current X rotation angle
    private float yRotation = 0f; // Current Y rotation angle

    private bool isPaused = false;

    private Vector3 focalPoint; // The focal point around which the camera orbits (optional for reference)

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
        focalPoint = new Vector3(0, transform.position.y, 0);

        //var uiManager = FindObjectOfType<InGameUIManager>();
        //uiManager.OnPauseMenuOpen.AddListener(() => { isPaused = true; });
        //uiManager.OnPauseMenuClose.AddListener(() => { isPaused = false; Cursor.lockState = CursorLockMode.Locked; });
    }

    void Update()
    {
        if (isPaused) return;

        // Rotate the camera using the mouse
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        xRotation -= mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limit vertical angle to avoid flipping

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

        // Move the camera relative to its own orientation
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 movement = transform.right * horizontalInput + transform.forward * verticalInput;
        movement.y = 0;
        movement = movement.normalized * moveSpeed * Time.deltaTime;

        transform.position += movement;

        // Ensure the camera doesn't move farther than 10 units from the target
        if (focalPoint != null)
        {
            float currentDistance = Vector3.Distance(transform.position, focalPoint);
            if (currentDistance > maxDistance)
            {
                Vector3 direction = (transform.position - focalPoint).normalized;
                transform.position = focalPoint + direction * maxDistance;
            }
        }
    }
}
