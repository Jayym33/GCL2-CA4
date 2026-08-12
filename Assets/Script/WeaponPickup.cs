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

        // Equip the weapon
        if (weaponManager != null)
        {
            weaponManager.EquipWeapon(equippedWeapon);
        }

        // Remove the weapon from the floor
        gameObject.SetActive(false);
    }
}