using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Bullet Settings")]
    // The bullet prefab that will be spawned when firing
    public GameObject bulletPrefab;

    [Header("Weapon Stats")]
    // Speed at which the bullet travels
    public float bulletSpeed = 40f;

    // Number of shots the gun can fire per second
    public float fireRate = 3f;

    // Maximum bullets in one magazine
    public int magazineSize = 15;

    // Stores the next time the gun is allowed to shoot
    private float nextFireTime;

    // Tracks the current amount of ammo remaining
    private int currentAmmo;

    void Start()
    {
        // Fill the magazine when the game starts
        currentAmmo = magazineSize;
    }

    void Update()
    {
        // Check if the left mouse button is being held
        // and if enough time has passed before firing again
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Fire();

            // Calculate the next available firing time
            nextFireTime = Time.time + (1f / fireRate);
        }
    }

    // Handles the shooting behaviour
    void Fire()
    {
        // Prevent shooting if no ammo is left
        if (currentAmmo <= 0)
        {
            Debug.Log("Out of Ammo!");
            return;
        }

        // Reduce ammo by one
        currentAmmo--;

        // Create a new bullet at the gun's position
        GameObject bullet = Instantiate(
            bulletPrefab,
            transform.position,
            transform.rotation);

        // Get the Rigidbody attached to the bullet
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        // Launch the bullet forward
        if (rb != null)
        {
            rb.linearVelocity = transform.forward * bulletSpeed;
        }

        Debug.Log("Bang! Ammo Left: " + currentAmmo);
    }
}