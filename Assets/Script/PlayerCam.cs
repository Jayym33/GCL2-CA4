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
        //get the curser to be in the middle of the screen + invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //getting the mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensorX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensorY;

        //setting the rotation
        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //rotate the camera + the orientation of it
        //rotate x axis
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        //rotate y axis
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

    }
}
