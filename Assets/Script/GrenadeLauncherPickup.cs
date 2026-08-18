using UnityEngine;

public class GrenadeLauncherPickup : MonoBehaviour
{
    private bool playerNearby = false;
    private WeaponManager weaponManager;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            PickUpGrenadeLauncher();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;

            weaponManager = other.GetComponent<WeaponManager>();

            Debug.Log("Press E to pick up grenade launcher");
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

    private void PickUpGrenadeLauncher()
    {
        if (weaponManager == null)
        {
            Debug.LogError("WeaponManager not found on Player!");
            return;
        }

        weaponManager.PickUpGrenadeLauncher();

        // Remove the grenade launcher from the floor
        gameObject.SetActive(false);
    }
}