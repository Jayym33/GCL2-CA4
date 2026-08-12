using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    // The weapon that will be equipped
    public GameObject weapon;

    // Where the weapon will be held
    public Transform weaponHolder;

    // Check if the player is close enough
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
        // Check if the player leaves
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }

    void PickUpWeapon()
    {
        // Put the weapon inside the WeaponHolder
        weapon.transform.SetParent(weaponHolder);

        // Reset position
        weapon.transform.localPosition = Vector3.zero;

        // Reset rotation
        weapon.transform.localRotation = Quaternion.identity;

        Debug.Log("Weapon picked up!");
    }
}