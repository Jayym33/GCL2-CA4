using UnityEngine;
using UnityEngine.AI; //Allow the enemy to use NavMesh for movement

public class BasicEnemyController : MonoBehaviour
{
    public Transform player;           // Stores reference to the player's Transform (position, rotation)
    public float speed = 3f;           // How fast the enemy moves toward the player
    public float detectionRange = 10f; // How close the player must be to the enemy for the enemy to be triggered
    public int health = 3;             // Enemy health

    private Rigidbody rb;              // Reference to the Rigidbody component (used for physics movement)

    void Start()
    {
        // Get the Rigidbody component attached to this enemy GameObject
        rb = GetComponent<Rigidbody>();

        // If the player was not manually assigned in the Inspector
        if (player == null)
        {
            // Find the GameObject in the scene with the tag "Player"
            // Then get its Transform component and store it
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        // Calculate the distance between the enemy and player using 3D positions
        float distance = Vector3.Distance(transform.position, player.position);

        // If the player is within the detection range, enemy will chase the player
        if (distance < detectionRange)
        {
            ChasePlayer(); // Call the chase function for the enemy
        }
        else
        {
            Idle(); // Otherwise, the player will stay idle
        }
    }

    void ChasePlayer()
    {
        // For the enemy to know the direction the player is at
        // (player position - enemy position = direction toward player)
        Vector3 direction = (player.position - transform.position).normalized;

        // Move enemy using Rigidbody velocity
        // Only the movement X and Z is changed so the enemy moves on the ground and doesn't affect the up/down movement
        // Y velocity is kept the same so gravity still works
        rb.linearVelocity = new Vector3(direction.x * speed, rb.linearVelocity.y, direction.z * speed);

        // Rotate the enemy to face the player so it looks at the player
        transform.LookAt(player);
    }

    void Idle()
    {
        // Stop horizontal movement (X and Z)
        // Keep Y velocity so gravity is unaffected
        rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
    }

    // Function that gets called when the enemy takes damage from the bullets
    public void TakeDamage(int damage)
    {
        // Reduce health by the damage amount
        health -= damage;

        // Check if health has dropped to 0 or below
        if (health <= 0)
        {
            Die(); // Call death function
        }
    }

    void Die()
    {
        // When enemy dies, destroy this enemy GameObject (removes it from the scene)
        Destroy(gameObject);
    }
}
