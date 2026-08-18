using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [Header("Ammo Settings")]
    public int ammoAmount = 15;

    private bool playerNearby = false;
    private Gun gun;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            PickUpAmmo();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;

            WeaponManager weaponManager = other.GetComponent<WeaponManager>();

            if (weaponManager != null)
            {
                if (weaponManager.pistol != null)
                {
                    gun = weaponManager.pistol.GetComponent<Gun>();
                }
            }

            Debug.Log("Press E to pick up ammo");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            gun = null;
        }
    }

    private void PickUpAmmo()
    {
        if (gun == null)
        {
            Debug.Log("You don't have a pistol yet!");
            return;
        }

        gun.AddAmmo(ammoAmount);

        // Remove ammo pickup from the floor
        gameObject.SetActive(false);
    }
}