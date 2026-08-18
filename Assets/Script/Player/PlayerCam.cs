using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [Header("Mouse Sensitivity")]
    public float sensorX = 200f;
    public float sensorY = 200f;

    [Header("Player")]
    public Transform orientation;
    public Transform playerBody;

    private float xRotation;
    private float yRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        yRotation = playerBody.eulerAngles.y;
    }

    void Update()
    {
        // Get mouse movement
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensorX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensorY;

        // Horizontal rotation
        yRotation += mouseX;

        // Vertical rotation
        xRotation -= mouseY;

        // Limit vertical camera movement
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotate player left/right
        playerBody.rotation = Quaternion.Euler(0f, yRotation, 0f);

        // Rotate movement orientation
        orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);

        // Camera looks up/down
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);


    }

    public void ResetCamera()
    {
        xRotation = 0f;
        yRotation = playerBody.eulerAngles.y;

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}