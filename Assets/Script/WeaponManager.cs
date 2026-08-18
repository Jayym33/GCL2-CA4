using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Weapon Holder")]
    public Transform weaponHolder;

    [Header("Equipped Weapons")]
    public GameObject bat;
    public GameObject pistol;
    public GameObject grenadeLauncher;

    // Tracks which weapons the player has picked up
    private bool hasBat = false;
    private bool hasPistol = false;
    private bool hasGrenadeLauncher = false;

    void Start()
    {
        // Make sure ALL weapons are hidden when the game starts
        if (bat != null)
            bat.SetActive(false);

        if (pistol != null)
            pistol.SetActive(false);

        if (grenadeLauncher != null)
            grenadeLauncher.SetActive(false);

        Debug.Log("All weapons hidden at start.");
    }

    void Update()
    {
        // =========================
        // NUMBER KEYS
        // =========================

        // 1 = Pistol
        if (hasPistol && Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipPistol();
        }

        // 2 = Bat
        if (hasBat && Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipBat();
        }

        // 3 = Grenade Launcher
        if (hasGrenadeLauncher && Input.GetKeyDown(KeyCode.Alpha3))
        {
            EquipGrenadeLauncher();
        }

        // =========================
        // MOUSE SCROLL
        // =========================

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f && hasPistol)
        {
            EquipPistol();
        }

        if (scroll < 0f && hasBat)
        {
            EquipBat();
        }
    }

    // =========================
    // PICKUPS
    // =========================

    public void PickUpPistol()
    {
        hasPistol = true;

        Debug.Log("Picked up pistol!");

        EquipPistol();
    }

    public void PickUpBat()
    {
        hasBat = true;

        Debug.Log("Picked up bat!");

        EquipBat();
    }

    public void PickUpGrenadeLauncher()
    {
        hasGrenadeLauncher = true;

        Debug.Log("Picked up grenade launcher!");

        EquipGrenadeLauncher();
    }

    // =========================
    // EQUIP WEAPONS
    // =========================

    private void EquipPistol()
    {
        if (bat != null)
            bat.SetActive(false);

        if (grenadeLauncher != null)
            grenadeLauncher.SetActive(false);

        if (pistol != null)
            pistol.SetActive(true);

        Debug.Log("Equipped Pistol");
    }

    private void EquipBat()
    {
        if (pistol != null)
            pistol.SetActive(false);

        if (grenadeLauncher != null)
            grenadeLauncher.SetActive(false);

        if (bat != null)
            bat.SetActive(true);

        Debug.Log("Equipped Bat");
    }

    private void EquipGrenadeLauncher()
    {
        if (pistol != null)
            pistol.SetActive(false);

        if (bat != null)
            bat.SetActive(false);

        if (grenadeLauncher != null)
            grenadeLauncher.SetActive(true);

        Debug.Log("Equipped Grenade Launcher");
    }
}