using UnityEngine;

// This script handles bullet collision and damage
public class Bullet : MonoBehaviour
{
    // Amount of damage dealt by this bullet
    public int damage = 20;

    // Destroy the bullet automatically after a few seconds
    public float destroyAfter = 5f;

    void Start()
    {
        // Prevent bullets from staying in the scene forever
        Destroy(gameObject, destroyAfter);
    }

    // Called when the bullet collides with another object
    private void OnCollisionEnter(Collision collision)
    {
        // Try to get the BasicEnemyController script from the object that was hit
        BasicEnemyController enemy = collision.gameObject.GetComponent<BasicEnemyController>();

        // If the object has the enemy script, deal damage
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        // Destroy the bullet after hitting anything
        Destroy(gameObject);
    }
}