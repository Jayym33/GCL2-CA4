using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // Where the weapons are held
    public Transform weaponHolder;

    // Weapons
    public GameObject pistol;
    public GameObject bat;

    // Which weapons the player has picked up
    private bool hasPistol = false;
    private bool hasBat = false;

    // Currently equipped weapon
    private GameObject currentWeapon;

    void Start()
    {
        // Hide both weapons at the start
        pistol.SetActive(false);
        bat.SetActive(false);
    }

    void Update()
    {
        // Press 1 to equip pistol
        if (Input.GetKeyDown(KeyCode.Alpha1) && hasPistol)
        {
            EquipWeapon(pistol);
        }

        // Press 2 to equip bat
        if (Input.GetKeyDown(KeyCode.Alpha2) && hasBat)
        {
            EquipWeapon(bat);
        }

        // Scroll the mouse wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        // Scroll UP = pistol
        if (scroll > 0f && hasPistol)
        {
            EquipWeapon(pistol);
        }

        // Scroll DOWN = bat
        if (scroll < 0f && hasBat)
        {
            EquipWeapon(bat);
        }
    }

    // Equip a weapon
    public void EquipWeapon(GameObject weaponToEquip)
    {
        // Hide current weapon
        if (currentWeapon != null)
        {
            currentWeapon.SetActive(false);
        }

        // Show new weapon
        weaponToEquip.SetActive(true);

        // Remember current weapon
        currentWeapon = weaponToEquip;
    }

    // Give player the pistol
    public void PickUpPistol()
    {
        hasPistol = true;

        Debug.Log("Picked up pistol!");
    }

    // Give player the bat
    public void PickUpBat()
    {
        hasBat = true;

        Debug.Log("Picked up bat!");
    }
}