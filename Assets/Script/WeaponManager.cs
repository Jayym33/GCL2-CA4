using UnityEngine;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour
{
    [Header("Weapon Holder")]
    public Transform weaponHolder;

    [Header("Equipped Weapons")]
    public GameObject bat;
    public GameObject pistol;
    public GameObject grenadeLauncher;

    // Weapons the player has picked up, in pickup order
    private List<GameObject> collectedWeapons = new List<GameObject>();

    // Current weapon index
    private int currentWeaponIndex = -1;

    void Start()
    {
        // Hide all weapons at the start
        if (bat != null)
            bat.SetActive(false);

        if (pistol != null)
            pistol.SetActive(false);

        if (grenadeLauncher != null)
            grenadeLauncher.SetActive(false);
    }

    void Update()
    {
        // =========================
        // NUMBER KEYS
        // =========================

        // 1 = First weapon picked up
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeaponByIndex(0);
        }

        // 2 = Second weapon picked up
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipWeaponByIndex(1);
        }

        // 3 = Third weapon picked up
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EquipWeaponByIndex(2);
        }


        // =========================
        // SCROLL WHEEL
        // =========================

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
        {
            // Scroll UP = previous weapon
            SwitchWeapon(-1);
        }

        if (scroll < 0f)
        {
            // Scroll DOWN = next weapon
            SwitchWeapon(1);
        }
    }


    // =====================================================
    // PICKUPS
    // =====================================================

    public void PickUpPistol()
    {
        AddWeapon(pistol);

        Debug.Log("Picked up pistol!");

        // Equip immediately
        EquipWeaponByIndex(currentWeaponIndex);
    }

    public void PickUpBat()
    {
        AddWeapon(bat);

        Debug.Log("Picked up bat!");

        // Equip immediately
        EquipWeaponByIndex(currentWeaponIndex);
    }

    public void PickUpGrenadeLauncher()
    {
        AddWeapon(grenadeLauncher);

        Debug.Log("Picked up grenade launcher!");

        // Equip immediately
        EquipWeaponByIndex(currentWeaponIndex);
    }


    // =====================================================
    // ADD WEAPON
    // =====================================================

    private void AddWeapon(GameObject weapon)
    {
        if (weapon == null)
        {
            Debug.LogError("Weapon is not assigned in WeaponManager!");
            return;
        }

        // Don't add the same weapon twice
        if (collectedWeapons.Contains(weapon))
        {
            Debug.Log("Already picked up this weapon.");
            return;
        }

        // Add weapon to the list
        collectedWeapons.Add(weapon);

        // The newly picked-up weapon becomes the current weapon
        currentWeaponIndex = collectedWeapons.Count - 1;
    }


    // =====================================================
    // NUMBER KEY EQUIPPING
    // =====================================================

    private void EquipWeaponByIndex(int index)
    {
        // Make sure that weapon exists
        if (index < 0 || index >= collectedWeapons.Count)
        {
            return;
        }

        currentWeaponIndex = index;

        // Hide every weapon
        HideAllWeapons();

        // Show selected weapon
        GameObject weapon = collectedWeapons[currentWeaponIndex];

        if (weapon != null)
        {
            weapon.SetActive(true);

            Debug.Log(
                "Equipped weapon " +
                (currentWeaponIndex + 1) +
                ": " +
                weapon.name
            );
        }
    }


    // =====================================================
    // SCROLL SWITCHING
    // =====================================================

    private void SwitchWeapon(int direction)
    {
        // No weapons collected
        if (collectedWeapons.Count == 0)
        {
            return;
        }

        int newIndex = currentWeaponIndex + direction;

        // Wrap around
        if (newIndex >= collectedWeapons.Count)
        {
            newIndex = 0;
        }

        if (newIndex < 0)
        {
            newIndex = collectedWeapons.Count - 1;
        }

        EquipWeaponByIndex(newIndex);
    }


    // =====================================================
    // HIDE ALL WEAPONS
    // =====================================================

    private void HideAllWeapons()
    {
        if (bat != null)
            bat.SetActive(false);

        if (pistol != null)
            pistol.SetActive(false);

        if (grenadeLauncher != null)
            grenadeLauncher.SetActive(false);
    }
}