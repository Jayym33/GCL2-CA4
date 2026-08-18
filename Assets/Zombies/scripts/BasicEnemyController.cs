using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    // The player's Transform (position, rotation,scale)
    public Transform player;

    // How fast the zombie moves
    public float speed = 2.5f;

    // How far away the zombie can detect the player
    public float detectionRange = 10f;

    // How close the zombie gets to before it stops running
    public float stoppingDistance = 1f;

    // How close the zombie needs to be to attack
    public float attackRange = 1.5f;

    // How much damage each attack does
    public int attackDamage = 5;

    // Time between attacks
    public float attackCooldown = 1.5f;

    // How much health the zombie has
    public int health = 20;

    // Keeps track of when the zombie can attack again
    private float nextAttackTime = 0f;

    // How long the attack animation lasts
    public float attackAnimationTime = 0.8f;

    // Tracks when the current attack animation should finish
    private float attackAnimationEndTime = 0f;

    // Reference to the zombie's Rigidbody
    private Rigidbody rb;

    // Reference to the zombie's Animator
    private Animator animator;

    // Save the zombie's original position and rotation
    private Vector3 startingPosition;
    private Quaternion startingRotation;

    void Start()
    {
        // Saves the zombies current position
        startingPosition = transform.position;
        startingRotation = transform.rotation;

        // Find the Rigidbody attached to the zombie
        rb = GetComponent<Rigidbody>();

        // Find the Animator on the zombie or its children
        animator = GetComponentInChildren<Animator>();

        // Check if the Rigidbody exists
        if (rb == null)
        {
            Debug.LogError("No Rigidbody found on the Zombie!");
        }

        // Check if the Animator exists
        if (animator == null)
        {
            Debug.LogError("No Animator found on the Zombie or its children!");
        }

        // Check if the Player has been assigned
        if (player == null)
        {
            Debug.LogError("Player has NOT been assigned in the Inspector!");
        }
    }


    void Update()
    {
        // Stop if there is no player assigned
        if (player == null)
        {
            return;
        }

        // Calculate the distance between the zombie and player
        float distance = Vector3.Distance(transform.position,player.position);

        // Checks if player is close enough to be detected by the zombie
        if (distance <= detectionRange)
        {
            // Checks if player is close enough for the zombie to attack
            if (distance <= attackRange)
            {
                // Stop the zombie's movement  
                Idle();

                // Calculates the direction of the player from the zombie
                Vector3 direction = player.position - transform.position;

                // Ignores vertical movement so it doesn't fly
                direction.y = 0;

                // Rotates the zombie to face the player
                if (direction != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(direction);
                }

                // Stop the Run animation
                if (animator != null)
                {
                    // Stop chasing, stop the Run animation
                    animator.SetBool("IsChasing", false);

                    // Start the Attack animation
                    animator.SetBool("IsAttacking", true);
                }

                // Deal damage to the player
                AttackPlayer();
            }
            else
            {
                // Player is detected but too far away to attack
                ChasePlayer();

                // Play Run animation
                if (animator != null)
                {
                    // Zombie continues to chase and run after player
                    animator.SetBool("IsChasing", true);

                    // Make sure Attack is turned off
                    animator.SetBool("IsAttacking", false);
                }
            }
        }
        else
        {
            // Player is too far away and cannot be detected by zombie, start Idleing
            Idle();

            // Play Idle animation
            if (animator != null)
            {
                // Zombie does not chase or attack player as they are too far
                animator.SetBool("IsChasing", false);
                animator.SetBool("IsAttacking", false);
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

            // Move the zombie towards the player
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
            // Keep Y velocity so gravity still works
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

    void AttackPlayer()
    {
        // Check if the zombie can attack again
        if (Time.time >= nextAttackTime)
        {
            // Find the PlayerHealth component attached to the player
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

            // Check if PlayerHealth exists
            if (playerHealth != null)
            {
                // Deal damage to the player
                playerHealth.TakeDamage(attackDamage);

                Debug.Log("Zombie attacked the player!");
            }
            else
            {
                Debug.LogWarning("PlayerHealth was not found on the Player!");
            }

            // Set the next attack time
            nextAttackTime = Time.time + attackCooldown;
        }
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

    public void ResetZombie()
    {
        // Reset position and rotation
        transform.position = startingPosition;
        transform.rotation = startingRotation;

        // Stop the zombie's movement
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Reset attack timer
        nextAttackTime = 0f;
        attackAnimationEndTime = 0f;

        // Reset animations
        if (animator != null)
        {
            animator.SetBool("IsChasing", false);
            animator.SetBool("IsAttacking", false);
        }

        Debug.Log(gameObject.name + " reset to starting position.");
    }

    void Die()
    {
        // Remove the zombie from the scene
        Destroy(gameObject);
    }
}
