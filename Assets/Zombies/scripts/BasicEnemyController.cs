using UnityEngine;
using UnityEngine.AI; //Allow the enemy to use NavMesh for movement

public class BasicEnemyController : MonoBehaviour
{
    // The player's Transform
    public Transform player;

    // How fast the zombie moves
    public float speed = 2f;

    // How far away the player can be before the zombie notices them
    public float detectionRange = 10f;

    // How close the zombie can get to the player
    public float stoppingDistance = 2f;

    // How much health the zombie has
    public int health = 3;

    // Reference to the zombie's Rigidbody
    private Rigidbody rb;

    // Reference to the zombie's Animator
    private Animator animator;


    void Start()
    {
        // Find the Rigidbody attached to the zombie
        rb = GetComponent<Rigidbody>();

        // Find the Animator attached to the zombie
        animator = GetComponent<Animator>();

        // Check if the Rigidbody exists
        if (rb == null)
        {
            Debug.LogError("No Rigidbody found on the Zombie!");
        }

        // Check if the Animator exists
        if (animator == null)
        {
            Debug.LogError("No Animator found on the Zombie!");
        }

        // Check if the Player has been assigned
        if (player == null)
        {
            Debug.LogError("Player has NOT been assigned in the Inspector!");
        }
    }


    void Update()
    {
        // Stop the code if there is no player assigned
        if (player == null)
        {
            return;
        }

        // Calculate the distance between the zombie and player
        float distance = Vector3.Distance(transform.position,player.position);

        // Check if the player is within the detection range
        if (distance <= detectionRange)
        {
            // Chase the player
            ChasePlayer();

            // Tell the Animator to play the Run animation
            if (animator != null)
            {
                animator.SetBool("IsChasing", true);
            }
        }
        else
        {
            // Player is too far away, so stop moving
            Idle();

            // Tell the Animator to play the Idle animation
            if (animator != null)
            {
                animator.SetBool("IsChasing", false);
            }
        }
    }


    void ChasePlayer()
    {
        // Stop if the Rigidbody doesn't exist
        if (rb == null)
        {
            return;
        }

        // Calculate the direction from the zombie to the player
        Vector3 direction = player.position - transform.position;

        // Ignore the Y axis so the zombie stays on the ground
        direction.y = 0;

        // Calculate the distance to the player
        float distance = direction.magnitude;

        // Only move if the zombie is outside the stopping distance
        if (distance > stoppingDistance)
        {
            // Make the direction have a length of 1
            direction.Normalize();

            // Move the zombie toward the player
            // Keep the Y velocity so gravity still works
            rb.linearVelocity = new Vector3(direction.x * speed,rb.linearVelocity.y,direction.z * speed);

            // Turn the zombie toward the player
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }
        else
        {
            // Stop horizontal movement when close to the player
            // Keep Y velocity for gravity
            rb.linearVelocity = new Vector3(0,rb.linearVelocity.y,0);
        }
    }


    void Idle()
    {
        // Stop if the Rigidbody doesn't exist
        if (rb == null)
        {
            return;
        }

        // Stop horizontal movement
        // Keep Y velocity so gravity still works
        rb.linearVelocity = new Vector3(0,rb.linearVelocity.y,0);
    }


    public void TakeDamage(int damage)
    {
        // Reduce the zombie's health
        health -= damage;

        // Check if the zombie has no health left
        if (health <= 0)
        {
            Die();
        }
    }


    void Die()
    {
        // Remove the zombie from the scene
        Destroy(gameObject);
    }
}
