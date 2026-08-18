using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    // The player's starting position
    public Transform startingPoint;

    // The checkpoint the player will respawn at
    private Transform currentCheckpoint;

    // Player's Rigidbody
    private Rigidbody rb;

    // Player's camera script
    private PlayerCam playerCam;

    void Start()
    {
        // Get the Rigidbody from the player
        rb = GetComponent<Rigidbody>();

        // Get the PlayerCam script
        playerCam = FindFirstObjectByType<PlayerCam>();

        // Set the starting point as the first checkpoint
        currentCheckpoint = startingPoint;
    }

    void Update()
    {
        // If the player falls below this height
        if (transform.position.y < -10f)
        {
            // Respawn the player
            Respawn();
        }
    }

    // Changes the current checkpoint
    public void SetCheckpoint(Transform newCheckpoint)
    {
        // Save the new checkpoint
        currentCheckpoint = newCheckpoint;

        // Show a message in the Console
        Debug.Log("Checkpoint reached!");
    }

    // Moves the player back to the checkpoint
    public void Respawn()
    {
        // Stop the player's movement
        rb.linearVelocity = Vector3.zero;

        // Stop the player's rotation
        rb.angularVelocity = Vector3.zero;

        // Move the player to the checkpoint
        transform.position = currentCheckpoint.position;

        // Rotate the player to match the checkpoint
        transform.rotation = currentCheckpoint.rotation;

        // Reset the camera
        if (playerCam != null)
        {
            playerCam.ResetCamera();
        }

        // Find all living zombies in the scene
        BasicEnemyController[] zombies =
            FindObjectsByType<BasicEnemyController>(
                FindObjectsSortMode.None
            );

        // Reset each zombie
        foreach (BasicEnemyController zombie in zombies)
        {
            zombie.ResetZombie();
        }
    }
}

