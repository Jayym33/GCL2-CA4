using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    public PlayerMovement playerController;

    [Header("Crouch")]
    public float normalHeight = 2f;
    public float crouchHeight = 1f;

    [Header("Speed")]
    public float crouchSpeed = 2.5f;

    [Header("Camera")]
    public Transform playerCamera;
    public float crouchCameraOffset = 0.5f;
    public float cameraMoveSpeed = 8f;

    private CapsuleCollider capsuleCollider;
    private float normalSpeed;
    private Vector3 normalCameraPosition;
    private bool crouching;

    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();

        normalHeight = capsuleCollider.height;
        normalSpeed = playerController.moveSpeed;

        // Remember the camera's original local position
        normalCameraPosition = playerCamera.localPosition;
    }

    void Update()
    {
        // Toggle crouch with Left Ctrl
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            crouching = !crouching;

            if (crouching)
            {
                Crouch();
            }
            else
            {
                Stand();
            }
                
        }

        // Move camera smoothly
        Vector3 targetCameraPosition = normalCameraPosition;

        if (crouching)
        {
            targetCameraPosition.y -= crouchCameraOffset;
        }

        playerCamera.localPosition = Vector3.Lerp(playerCamera.localPosition,targetCameraPosition,cameraMoveSpeed * Time.deltaTime);
    }

    void Crouch()
    {
        capsuleCollider.height = crouchHeight;

        capsuleCollider.center = new Vector3(0f,-(normalHeight - crouchHeight) / 2f,0f);

        playerController.moveSpeed = crouchSpeed;

        Debug.Log("Crouching!");
    }

    void Stand()
    {
        capsuleCollider.height = normalHeight;

        capsuleCollider.center = Vector3.zero;

        playerController.moveSpeed = normalSpeed;

        Debug.Log("Standing!");
    }
}