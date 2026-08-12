using UnityEngine;
using UnityEngine.AI; //Allow the enemy to use NavMesh for movement

public class BasicEnemyController : MonoBehaviour
{
    // The player's Transform
    public Transform player;

    // How fast the zombie moves
    public float speed = 2f;

    // How far away the zombie can detect the player
    public float detectionRange = 10f;

    // How close the zombie gets before stopping
    public float stoppingDistance = 2f;

    // How close the zombie needs to be to attack
    public float attackRange = 2f;

    // How much damage each attack does
    public int attackDamage = 5;

    // Time between attacks
    public float attackCooldown = 1.5f;

    // How much health the zombie has
    public int health = 3;

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



    void Start()
    {
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
        // Stop the code if there is no player
        if (player == null)
        {
            return;
        }

        // Turn off the attack animation after its duration
        if (animator != null && Time.time >= attackAnimationEndTime)
        {
            animator.SetBool("IsAttacking", false);
        }

        // Calculate the distance between the zombie and player
        float distance = Vector3.Distance(transform.position,player.position);

        // Check if the player is within detection range
        if (distance <= detectionRange)
        {
            // Check if the player is close enough to attack
            if (distance <= attackRange)
            {
                // Stop moving while attacking
                Idle();

                // Tell the Animator to stop the Run animation
                if (animator != null)
                {
                    animator.SetBool("IsChasing", false);
                    animator.SetBool("IsAttacking", true);
                }

                // Deal damage to the player
                AttackPlayer();
            }
            else
            {
                // Player is nearby but too far away to attack
                // Chase the player
                ChasePlayer();

                // Play the Run animation
                if (animator != null)
                {
                    animator.SetBool("IsChasing", true);
                }
            }
        }
        else
        {
            // Player is outside the detection range
            // Stop the zombie
            Idle();

            // Play the Idle animation
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
            rb.linearVelocity = new Vector3(direction.x * speed,rb.linearVelocity.y,direction.z * speed
            );

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
        // Check if enough time has passed since the previous attack
        if (Time.time >= nextAttackTime)
        {
            // Tell the Animator to play the attack animation
            if (animator != null)
            {
                animator.SetBool("IsAttacking", true);

                // Set when the attack animation should finish
                attackAnimationEndTime = Time.time + attackAnimationTime;
            }

            // Find the player's health component
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

            // Check if the player has PlayerHealth
            if (playerHealth != null)
            {
                // Deal damage to the player
                playerHealth.TakeDamage(attackDamage);
            }

            // Set the next time the zombie can attack
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


    void Die()
    {
        // Remove the zombie from the scene
        Destroy(gameObject);
    }
}
