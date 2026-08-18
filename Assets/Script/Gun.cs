using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;

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
        if (!gameObject.activeInHierarchy)
            return;

        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Fire();

            nextFireTime = Time.time + (1f / fireRate);
        }
    }

    void Fire()
    {
        if (currentAmmo <= 0)
        {
            Debug.Log("Out of Ammo!");
            return;
        }

        currentAmmo--;

        if (bulletPrefab == null)
        {
            Debug.LogWarning("Bullet Prefab is not assigned!");
            return;
        }

        if (firePoint == null)
        {
            Debug.LogWarning("Fire Point is not assigned!");
            return;
        }

        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation
        );

        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * bulletSpeed;
        }
        else
        {
            Debug.LogWarning("Bullet prefab does not have a Rigidbody!");
        }

        if (crosshair != null)
        {
            crosshair.OnShoot();
        }

        Debug.Log("Bang! Ammo Left: " + currentAmmo);
    }

    public void AddAmmo(int amount)
    {
        currentAmmo += amount;

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