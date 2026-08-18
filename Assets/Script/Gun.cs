using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;

    [Header("Weapon Stats")]
    public float bulletSpeed = 40f;
    public float fireRate = 3f;
    public int magazineSize = 15;

    [Header("Crosshair")]
    public Crosshair crosshair;

    private float nextFireTime;
    private int currentAmmo;

    void Start()
    {
        currentAmmo = magazineSize;
    }

    void Update()
    {
        // Only fire when the pistol is currently active
        if (!gameObject.activeInHierarchy)
            return;

        // Hold left mouse button to fire
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Fire();

            nextFireTime = Time.time + (1f / fireRate);
        }
    }

    void Fire()
    {
        // No ammo
        if (currentAmmo <= 0)
        {
            Debug.Log("Out of Ammo!");
            return;
        }

        // Use one bullet
        currentAmmo--;

        // Spawn bullet
        if (bulletPrefab != null)
        {
            GameObject bullet = Instantiate(
                bulletPrefab,
                transform.position,
                transform.rotation
            );

            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.linearVelocity = transform.forward * bulletSpeed;
            }
            else
            {
                Debug.LogWarning("Bullet prefab does not have a Rigidbody!");
            }

            // Make crosshair expand when shooting
            if (crosshair != null)
            {
                crosshair.OnShoot();
            }
        }
        else
        {
            Debug.LogWarning("Bullet Prefab is not assigned!");
        }

        Debug.Log("Bang! Ammo Left: " + currentAmmo);
    }

    public void AddAmmo(int amount)
    {
        currentAmmo += amount;

        // Don't go above magazine size
        if (currentAmmo > magazineSize)
        {
            currentAmmo = magazineSize;
        }

        Debug.Log("Ammo picked up! Ammo: " + currentAmmo);
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }
}