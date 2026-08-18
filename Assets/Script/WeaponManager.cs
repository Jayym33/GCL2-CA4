using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Weapon Holder")]
    public Transform weaponHolder;

    [Header("Equipped Weapons")]
    public GameObject bat;
    public GameObject pistol;

    // Tracks which weapons the player has picked up
    private bool hasBat = false;
    private bool hasPistol = false;

    void Start()
    {
        // Hide both weapons at the start
        bat.SetActive(false);
        pistol.SetActive(false);
    }

    void Update()
    {
        // Press 1 = Pistol
        if (hasPistol && Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipPistol();
        }

        // Press 2 = Bat
        if (hasBat && Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipBat();
        }

        // Mouse scroll
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        // Scroll up = Pistol
        if (scroll > 0f && hasPistol)
        {
            EquipPistol();
        }

        // Scroll down = Bat
        if (scroll < 0f && hasBat)
        {
            EquipBat();
        }
    }

    // Called when player picks up the pistol
    public void PickUpPistol()
    {
        hasPistol = true;

        Debug.Log("Picked up pistol!");

        // Equip pistol immediately
        EquipPistol();
    }

    // Called when player picks up the bat
    public void PickUpBat()
    {
        hasBat = true;

        Debug.Log("Picked up bat!");

        // Equip bat immediately
        EquipBat();
    }

    // Equip pistol
    private void EquipPistol()
    {
        bat.SetActive(false);
        pistol.SetActive(true);

        Debug.Log("Equipped Pistol");
    }

    // Equip bat
    private void EquipBat()
    {
        pistol.SetActive(false);
        bat.SetActive(true);

        Debug.Log("Equipped Bat");
    }
}