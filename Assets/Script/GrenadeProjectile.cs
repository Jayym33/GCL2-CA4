using UnityEngine;

public class GrenadeProjectile : MonoBehaviour
{
    [Header("Explosion Settings")]
    public float explosionRadius = 5f;
    public int explosionDamage = 100;

    [Header("Explosion Effect")]
    public GameObject explosionEffect;

    private bool hasExploded = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (hasExploded)
            return;

        Explode();
    }

    void Explode()
    {
        hasExploded = true;

        Debug.Log("GRENADE EXPLODED!");

        // Find everything inside the explosion radius
        Collider[] hitObjects = Physics.OverlapSphere(
            transform.position,
            explosionRadius
        );

        // For now, just show what objects were inside the explosion
        foreach (Collider hit in hitObjects)
        {
            Debug.Log(
                "Explosion hit: " + hit.gameObject.name
            );
        }

        // Create explosion visual effect if one is assigned
        if (explosionEffect != null)
        {
            Instantiate(
                explosionEffect,
                transform.position,
                Quaternion.identity
            );
        }

        // Destroy grenade
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(
            transform.position,
            explosionRadius
        );
    }
}