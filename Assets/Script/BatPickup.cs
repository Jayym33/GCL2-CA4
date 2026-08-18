using UnityEngine;

public class BatPickup : MonoBehaviour
{
    private bool playerNearby = false;
    private WeaponManager weaponManager;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            PickUpBat();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;

            weaponManager = other.GetComponent<WeaponManager>();

            Debug.Log("Press E to pick up bat");
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

    private void PickUpBat()
    {
        if (weaponManager == null)
        {
            Debug.LogError("WeaponManager not found on Player!");
            return;
        }

        weaponManager.PickUpBat();

        // Remove the bat from the floor
        gameObject.SetActive(false);
    }
}