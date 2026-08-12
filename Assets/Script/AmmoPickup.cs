using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    // How much ammo this pickup gives
    public int ammoAmount = 1;

    // Checks if the player is nearby
    private bool playerNearby = false;

    void Update()
    {
        // Press E to pick up ammo
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            PickUpAmmo();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the pickup area
        if (other.CompareTag("Player"))
        {
            playerNearby = true;

            Debug.Log("Press E to pick up ammo");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player leaves
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }

    void PickUpAmmo()
    {
        // Find the AmmoManager on the player
        AmmoManager ammoManager = FindFirstObjectByType<AmmoManager>();

        // Add ammo to the player
        if (ammoManager != null)
        {
            ammoManager.AddAmmo(ammoAmount);
        }

        // Remove the bullet from the floor
        Destroy(gameObject);
    }
}