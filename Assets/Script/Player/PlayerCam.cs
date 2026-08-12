using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensorX;
    public float sensorY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Lock the cursor to the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Get mouse movement
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensorX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensorY;

        // Rotate camera horizontally
        yRotation += mouseX;

        // Rotate camera vertically
        xRotation -= mouseY;

        // Stop the camera from looking too far up or down
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotate the camera
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        // Rotate the player's orientation
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    // Reset the camera rotation when the player respawns
    public void ResetCamera()
    {
        // Reset the camera's up/down rotation
        xRotation = 0f;

        // Get the player's current Y rotation
        yRotation = transform.parent.eulerAngles.y;

        // Reset camera rotation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        // Reset player orientation
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
