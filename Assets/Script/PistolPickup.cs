using UnityEngine;

public class PistolPickup : MonoBehaviour
{
    private bool playerNearby = false;
    private WeaponManager weaponManager;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            PickUpPistol();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;

            weaponManager = other.GetComponent<WeaponManager>();

            Debug.Log("Press E to pick up pistol");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            weaponManager = null;
        }
    }

    private void PickUpPistol()
    {
        if (weaponManager == null)
        {
            Debug.LogError("WeaponManager not found on Player!");
            return;
        }

        weaponManager.PickUpPistol();

        // Remove the pistol from the floor
        gameObject.SetActive(false);
    }
}