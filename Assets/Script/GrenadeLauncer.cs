using UnityEngine;

public class GrenadeLauncher : MonoBehaviour
{
    [Header("Grenade Settings")]
    public GameObject grenadePrefab;

    [Header("Weapon Stats")]
    public float grenadeSpeed = 20f;
    public float fireRate = 1f;
    public int magazineSize = 5;

    [Header("Explosion Settings")]
    public float explosionRadius = 5f;
    public int explosionDamage = 100;

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
        // Only fire when the grenade launcher is currently active
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
            Debug.Log("Out of Grenades!");
            return;
        }

        // Use one grenade
        currentAmmo--;

        // Spawn grenade
        if (grenadePrefab != null)
        {
            GameObject grenade = Instantiate(
                grenadePrefab,
                transform.position,
                transform.rotation
            );

            Rigidbody rb = grenade.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Launch grenade forward
                rb.linearVelocity = transform.forward * grenadeSpeed;
            }
            else
            {
                Debug.LogWarning(
                    "Grenade prefab does not have a Rigidbody!"
                );
            }

            // Give grenade its explosion settings
            GrenadeProjectile projectile =
                grenade.GetComponent<GrenadeProjectile>();

            if (projectile != null)
            {
                projectile.explosionRadius = explosionRadius;
                projectile.explosionDamage = explosionDamage;
            }

            // Make crosshair expand when shooting
            if (crosshair != null)
            {
                crosshair.OnShoot();
            }
        }
        else
        {
            Debug.LogWarning(
                "Grenade Prefab is not assigned!"
            );
        }

        Debug.Log(
            "Grenade Fired! Grenades Left: " + currentAmmo
        );
    }

    public void AddAmmo(int amount)
    {
        currentAmmo += amount;

        // Don't go above magazine size
        if (currentAmmo > magazineSize)
        {
            currentAmmo = magazineSize;
        }

        Debug.Log(
            "Grenades picked up! Grenades: " + currentAmmo
        );
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }
}