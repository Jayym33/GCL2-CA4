using UnityEngine;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    public Gun gun;
    public TMP_Text ammoText;

    void Update()
    {
        if (gun != null)
        {
            ammoText.text = gun.currentAmmo.ToString();
        }
    }
}