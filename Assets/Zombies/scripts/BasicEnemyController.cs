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
        // Calculate the direction from the enemy to the player
        Vector3 direction = player.position - transform.position;

        // Ignore the Y axis.
        // This prevents the zombie from trying to look upward or downward.
        direction.y = 0;

        // Calculate the distance between the zombie and player
        float distance = direction.magnitude;

        // Only move if the player is far enough away
        if (distance > 1.5f)
        {
            // Normalize the direction so the zombie moves at a consistent speed
            direction.Normalize();

            // Move toward the player
            // X and Z control ground movement
            // Y keeps the current gravity/falling velocity
            rb.linearVelocity = new Vector3(
                direction.x * speed,
                rb.linearVelocity.y,
                direction.z * speed
            );

            // Rotate the zombie to face the player
            // Only do this while the zombie is actually chasing
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }
        else
        {
            // Stop moving when the zombie gets close to the player
            rb.linearVelocity = new Vector3(
                0,
                rb.linearVelocity.y,
                0
            );
        }
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
