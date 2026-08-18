using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [Header("Camera Position")]
    public Transform cameraPos;

    [Header("Player")]
    public Transform playerBody;

    void Update()
    {
        // Follow the camera position
        transform.position = cameraPos.position;

        // Follow the player's horizontal rotation
        transform.rotation = Quaternion.Euler(
            0f,
            playerBody.eulerAngles.y,
            0f
        );
    }
}