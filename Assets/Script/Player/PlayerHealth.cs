using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;

    public GameObject deathScreen;

    private RespawnManager respawnManager;
    private PlayerMovement playerMovement;

    void Start()
    {
        currentHealth = maxHealth;

        respawnManager = GetComponent<RespawnManager>();
        playerMovement = GetComponent<PlayerMovement>();

        // Hide death screen when the game starts
        if (deathScreen != null)
        {
            deathScreen.SetActive(false);
        }

        // Lock cursor during gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    /*public void Heal(int amount)
    {
        currentHealth += amount;

        // Prevent health from going above max health
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        Debug.Log("Player Health: " + currentHealth);
    }*/

    private void Die()
    {
        Debug.Log("Player Died!");

        // Show death screen
        if (deathScreen != null)
        {
            deathScreen.SetActive(true);
        }

        // Stop player movement
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        // Show cursor
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void RespawnPlayer()
    {
        Debug.Log("RESPAWN BUTTON PRESSED");

        // Respawn at latest checkpoint
        if (respawnManager != null)
        {
            respawnManager.Respawn();
        }
        else
        {
            Debug.LogError("RespawnManager is missing from the Player!");
            return;
        }

        // Restore health
        currentHealth = maxHealth;

        // Hide death screen
        if (deathScreen != null)
        {
            deathScreen.SetActive(false);
        }

        // Enable player movement
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        // Lock cursor again
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Debug.Log("Player respawned!");
    }
}