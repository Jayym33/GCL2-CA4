using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    public Transform player;        // Stores reference to the player object
    public float speed = 2f;        // How fast the enemy moves
    public float detectionRange = 5f; // How close the player must be before enemy starts chasing
    public int health = 3;          // Enemy health 

    private Rigidbody2D rb;         // Reference to the Rigidbody2D component

    void Start()
    {
        // Get the Rigidbody2D component attached to this enemy
        rb = GetComponent<Rigidbody2D>();

        // If player is not manually assigned in Inspector, find it automatically
        if (player == null)
        {
            // Find the GameObject with tag "Player" and store its Transform
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        // Calculate the distance between the enemy and player
        float distance = Vector2.Distance(transform.position, player.position);

        // If player is within detection range, the enemy will chase the player
        if (distance < detectionRange)
        {
            ChasePlayer();
        }
        else
        {
            // If the player is far and not in the dectection range, the enemy will go back to idle
            Idle();
        }
    }

    void ChasePlayer()
    {
        // For the enemy to figure out the direction that the player is at
        Vector2 direction = (player.position - transform.position).normalized;

        // normalized direction ensures consistent speed (not faster diagonally)
        // Move the enemy towards the player in that direction and speed
        rb.linearVelocity = direction * speed;
    }

    void Idle()
    {
        // Stop all movement when idle
        rb.linearVelocity = Vector2.zero;
    }

    // This function is called when the enemy takes damage
    public void TakeDamage(int damage)
    {
        // Reduce health by the damage amount
        health -= damage;

        // Check if enemy health has reached 0 or below
        if (health <= 0)
        {
            Die(); // Call death function
        }
    }

    void Die()
    {
        // Destroy the enemy GameObject when enemy dies (removes it from the scene)
        Destroy(gameObject);
    }
}
