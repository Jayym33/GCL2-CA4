using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Weapon Holder")]
    public Transform weaponHolder;

    [Header("Weapons")]
    public GameObject bat;
    public GameObject gun;

    private bool hasGun = false;
    private bool usingGun = false;

    void Start()
    {
        // Player starts with the bat
        bat.SetActive(true);

        // Gun is hidden until picked up
        if (gun != null)
        {
            gun.SetActive(false);
        }
    }

    void Update()
    {
        // Right click switches weapons
        if (hasGun && Input.GetMouseButtonDown(1))
        {
            SwitchWeapon();
        }
    }

    public void PickUpGun(GameObject pickedUpGun)
    {
        gun = pickedUpGun;

        // Put gun into player's weapon holder
        gun.transform.SetParent(weaponHolder);

        hasGun = true;

        // Keep the bat equipped
        gun.SetActive(false);
        bat.SetActive(true);
        usingGun = false;

        Debug.Log("Picked up gun!");
    }

    void SwitchWeapon()
    {
        if (usingGun)
        {
            // Gun → Bat
            gun.SetActive(false);
            bat.SetActive(true);

            usingGun = false;

            Debug.Log("Switched to Bat");
        }
        else
        {
            // Bat → Gun
            bat.SetActive(false);
            gun.SetActive(true);

            usingGun = true;

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