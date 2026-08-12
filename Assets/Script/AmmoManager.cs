using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    // How much ammo the player currently has
    public int currentAmmo = 0;

    // Add ammo to the player's ammo count
    public void AddAmmo(int amount)
    {
        currentAmmo += amount;

        Debug.Log("Ammo: " + currentAmmo);
    }
}