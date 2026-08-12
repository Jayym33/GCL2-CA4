using UnityEngine;

public class GunPickup : MonoBehaviour
{
    private bool playerNearby = false;
    private WeaponManager weaponManager;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            weaponManager.PickUpGun(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;

            weaponManager = other.GetComponent<WeaponManager>();

            Debug.Log("Press E to pick up gun");
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
}