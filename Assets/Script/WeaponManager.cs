using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // Where the equipped weapon is held
    public Transform weaponHolder;

    // Equipped weapon models
    public GameObject pistol;
    public GameObject bat;

    // The weapon currently equipped
    private GameObject currentWeapon;

    void Start()
    {
        // Hide weapons when the game starts
        pistol.SetActive(false);
        bat.SetActive(false);
    }

    // Equip a weapon
    public void EquipWeapon(GameObject weaponToEquip)
    {
        // Hide the current weapon
        if (currentWeapon != null)
        {
            currentWeapon.SetActive(false);
        }

        // Show the new weapon
        weaponToEquip.SetActive(true);

        // Remember the equipped weapon
        currentWeapon = weaponToEquip;

        Debug.Log("Weapon equipped!");
    }
}