using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public int healAmount = 5;

    private bool playerNearby = false;
    private PlayerHealth playerHealth;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            HealPlayer();
        }
    }

    void HealPlayer()
    {
        if (playerHealth.currentHealth < playerHealth.maxHealth)
        {
            playerHealth.currentHealth += healAmount;

            // Prevent health from going above max health
            playerHealth.currentHealth =
                Mathf.Min(playerHealth.currentHealth, playerHealth.maxHealth);

            Debug.Log("Healed 5");

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth = other.GetComponent<PlayerHealth>();
            playerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            playerHealth = null;
        }
    }
}
