using UnityEngine;

public class GrenadeLauncher : MonoBehaviour
{
    [Header("Grenade Settings")]
    public GameObject grenadePrefab;

    [Header("Muzzle")]
    public Transform muzzle;

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
        // Only fire when grenade launcher is equipped
        if (!gameObject.activeInHierarchy)
            return;

        // Left click to fire
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            Fire();

            nextFireTime = Time.time + (1f / fireRate);
        }
    }

    void Fire()
    {
        if (currentAmmo <= 0)
        {
            Debug.Log("Out of Grenades!");
            return;
        }

        if (muzzle == null)
        {
            Debug.LogWarning("Muzzle is not assigned!");
            return;
        }

        if (grenadePrefab == null)
        {
            Debug.LogWarning("Grenade Prefab is not assigned!");
            return;
        }

        currentAmmo--;

        GameObject grenade = Instantiate(
            grenadePrefab,
            muzzle.position,
            muzzle.rotation
        );

        Rigidbody rb = grenade.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            rb.AddForce(
                muzzle.forward * grenadeSpeed,
                ForceMode.Impulse
            );

            Debug.Log(
                "Grenade launched! Direction: " + muzzle.forward
            );
        }
        else
        {
            Debug.LogError(
                "GRENADE HAS NO RIGIDBODY!"
            );
        }

        GrenadeProjectile projectile =
            grenade.GetComponent<GrenadeProjectile>();

        if (projectile != null)
        {
            projectile.explosionRadius = explosionRadius;
            projectile.explosionDamage = explosionDamage;
        }

        if (crosshair != null)
        {
            crosshair.OnShoot();
        }

        Debug.Log(
            "Grenade Fired! Grenades Left: " + currentAmmo
        );
    }

    public void AddAmmo(int amount)
    {
        currentAmmo += amount;

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