using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 15f;
    public float groundDrag = 5f;

    [Header("Ground Check")]
    public float playerheight = 2f;
    public LayerMask whatIsGround;
    bool isGrounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rigidPlayer;

    void Start()
    {
        rigidPlayer = GetComponent<Rigidbody>();

        rigidPlayer.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        // Ground check
        isGrounded = Physics.Raycast(transform.position,Vector3.down,playerheight * 0.5f + 0.2f,whatIsGround);

        PlayerInput();
        SpeedControl();

        // Apply drag
        if (isGrounded)
        {
            rigidPlayer.linearDamping = groundDrag;
        }
        else
        {
            rigidPlayer.linearDamping = 0;
        }
            

        // Jump ONLY with Space
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigidPlayer.AddForce(Vector3.up * jumpForce,ForceMode.Impulse);

            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        PlayerMove();
    }

    void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    void PlayerMove()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        moveDirection.y = 0f;

        rigidPlayer.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        Quaternion targetRotation = Quaternion.Euler(0f,orientation.eulerAngles.y,0f);

        rigidPlayer.MoveRotation(targetRotation);
    }

    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rigidPlayer.linearVelocity.x,0f,rigidPlayer.linearVelocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;

            rigidPlayer.linearVelocity = new Vector3(limitedVel.x,rigidPlayer.linearVelocity.y,limitedVel.z);
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