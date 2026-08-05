using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Gun Settings")]
    public GameObject bulletPrefab;
    public float bulletSpeed = 50f;
    public float fireRate = 3f;
    public int maxAmmo = 15;

    private int currentAmmo;
    private float nextFireTime = 0f;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        // Left Mouse Button
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        if (currentAmmo <= 0)
        {
            Debug.Log("Out of ammo!");
            return;
        }

        currentAmmo--;

        GameObject bullet = Instantiate(
            bulletPrefab,
            transform.position,
            transform.rotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = transform.forward * bulletSpeed;
        }

        Debug.Log("Bang! Ammo left: " + currentAmmo);
    }
}