using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    // The equipped version of this weapon
    public GameObject equippedWeapon;

    // Check if the player is nearby
    private bool playerNearby = false;

    void Update()
    {
        // Press E when near the weapon
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            PickUpWeapon();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the pickup area
        if (other.CompareTag("Player"))
        {
            playerNearby = true;

            Debug.Log("Press E to pick up weapon");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player leaves the pickup area
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }

    void PickUpWeapon()
    {
        // Find the WeaponManager on the player
        WeaponManager weaponManager = FindFirstObjectByType<WeaponManager>();

        if (weaponManager != null)
        {
            // Check which weapon was picked up
            if (equippedWeapon == weaponManager.pistol)
            {
                // Add pistol to inventory
                weaponManager.PickUpPistol();
            }
            else if (equippedWeapon == weaponManager.bat)
            {
                // Add bat to inventory
                weaponManager.PickUpBat();
            }
        }

        // Remove the weapon pickup from the floor
        gameObject.SetActive(false);
    }
}