using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;

    public GameObject deathScreen;

    [Header("Low Health Warning")]
    public Image lowHealthWarning;
    public int lowHealthThreshold = 5;
    public float blinkSpeed = 0.5f;

    private RespawnManager respawnManager;
    private PlayerMovement playerMovement;

    private Coroutine lowHealthCoroutine;

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

        // Make low health warning completely transparent
        if (lowHealthWarning != null)
        {
            Color color = lowHealthWarning.color;
            color.a = 0f;
            lowHealthWarning.color = color;
        }

        // Lock cursor during gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Check if health is low
        if (currentHealth > 0 && currentHealth <= lowHealthThreshold)
        {
            // Start blinking if it isn't already blinking
            if (lowHealthCoroutine == null)
            {
                lowHealthCoroutine = StartCoroutine(BlinkLowHealthWarning());
            }
        }
        else
        {
            // Stop blinking if health is no longer low
            if (lowHealthCoroutine != null)
            {
                StopCoroutine(lowHealthCoroutine);
                lowHealthCoroutine = null;
            }

            // Make vignette invisible
            if (lowHealthWarning != null)
            {
                Color color = lowHealthWarning.color;
                color.a = 0f;
                lowHealthWarning.color = color;
            }
        }
    }

    private IEnumerator BlinkLowHealthWarning()
    {
        while (currentHealth > 0 && currentHealth <= lowHealthThreshold)
        {
            if (lowHealthWarning != null)
            {
                // Fade IN
                yield return StartCoroutine(FadeVignette(0f, 1f));

                // Fade OUT
                yield return StartCoroutine(FadeVignette(1f, 0f));
            }
        }

        lowHealthCoroutine = null;
    }

    private IEnumerator FadeVignette(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;

        Color color = lowHealthWarning.color;
        color.a = startAlpha;
        lowHealthWarning.color = color;

        while (elapsedTime < blinkSpeed)
        {
            elapsedTime += Time.deltaTime;

            float alpha = Mathf.Lerp(
                startAlpha,
                endAlpha,
                elapsedTime / blinkSpeed
            );

            color.a = alpha;
            lowHealthWarning.color = color;

            yield return null;
        }

        // Make sure it reaches the exact final opacity
        color.a = endAlpha;
        lowHealthWarning.color = color;
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

        // Stop low health warning
        if (lowHealthCoroutine != null)
        {
            StopCoroutine(lowHealthCoroutine);
            lowHealthCoroutine = null;
        }

        // Hide vignette
        if (lowHealthWarning != null)
        {
            Color color = lowHealthWarning.color;
            color.a = 0f;
            lowHealthWarning.color = color;
        }

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

        // Reset health
        currentHealth = maxHealth;

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

        // Hide vignette
        if (lowHealthWarning != null)
        {
            Color color = lowHealthWarning.color;
            color.a = 0f;
            lowHealthWarning.color = color;
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