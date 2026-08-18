using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float jumpForce = 15.0f;
    public float groundDrag;

    [Header("Ground Check")]
    public float playerheight;
    public LayerMask whatIsGround;
    bool isGrounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rigidPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidPlayer = GetComponent<Rigidbody>();

        rigidPlayer.constraints =
            RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezeRotationZ;
    }

    // Update is called once per frame
    void Update()
    {
     
        // ground check
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerheight * 0.5f + 0.2f, whatIsGround);

        PlayerInput();
        SpeedControl();

        //apply drag
        if (isGrounded)
        {
            rigidPlayer.linearDamping = groundDrag;
        }
        else
        {
            rigidPlayer.linearDamping = 0;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rigidPlayer.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Apply instant upward force
            isGrounded = false; // Prevent mid-air jumps
        }
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void PlayerMove()
    {
        // Calculate movement direction based on orientation
        moveDirection = orientation.forward * verticalInput
                      + orientation.right * horizontalInput;

        // Prevent looking up/down from affecting movement
        moveDirection.y = 0f;

        // Move the player
        rigidPlayer.AddForce(
            moveDirection.normalized * moveSpeed * 10f,
            ForceMode.Force
        );

        // Rotate the player's body to match the camera/orientation direction
        Quaternion targetRotation = Quaternion.Euler(
            0f,
            orientation.eulerAngles.y,
            0f
        );

        rigidPlayer.MoveRotation(targetRotation);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rigidPlayer.linearVelocity.x, 0f, rigidPlayer.linearVelocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rigidPlayer.linearVelocity = new Vector3(limitedVel.x, rigidPlayer.linearVelocity.y, limitedVel.z);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
