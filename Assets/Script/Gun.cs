using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Aiming")]
    public Camera playerCamera;
    public float aimDistance = 100f;

    [Header("Weapon Stats")]
    public float bulletSpeed = 15f;
    public float fireRate = 3f;
    public int magazineSize = 15;

    [Header("Crosshair")]
    public Crosshair crosshair;

    private float nextFireTime;
    public int currentAmmo;

    void Start()
    {
        currentAmmo = magazineSize;

        // Automatically find the main camera
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
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

        currentAmmo--;

        // Check bullet prefab
        if (bulletPrefab == null)
        {
            Debug.LogWarning("Bullet Prefab is not assigned!");
            return;
        }

        // Check fire point
        if (firePoint == null)
        {
            Debug.LogWarning("Fire Point is not assigned!");
            return;
        }

        // Check camera
        if (playerCamera == null)
        {
            Debug.LogWarning("Player Camera is not assigned!");
            return;
        }

        // Create a ray from the center of the camera
        Ray ray = playerCamera.ViewportPointToRay(
            new Vector3(0.5f, 0.5f, 0f)
        );

        Vector3 targetPoint;

        // Check if the ray hits something
        if (Physics.Raycast(ray, out RaycastHit hit, aimDistance))
        {
            targetPoint = hit.point;
        }
        else
        {
            // If nothing is hit, shoot toward a point far away
            targetPoint = ray.origin + ray.direction * aimDistance;
        }

        // Calculate direction from the FirePoint to the crosshair target
        Vector3 shootDirection =
            (targetPoint - firePoint.position).normalized;

        // Spawn bullet at the FirePoint
        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            Quaternion.LookRotation(shootDirection)
        );

        // Give bullet velocity toward the crosshair
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = shootDirection * bulletSpeed;
        }
        else
        {
            Debug.LogWarning("Bullet prefab does not have a Rigidbody!");
        }

        // Expand crosshair
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