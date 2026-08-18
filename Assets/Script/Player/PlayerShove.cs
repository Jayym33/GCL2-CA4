using UnityEngine;

public class PlayerShove : MonoBehaviour
{
    [Header("Shove Settings")]
    public float shoveRange = 2f;
    public float shoveForce = 10f;
    public float shoveCooldown = 0.5f;

    [Header("Detection")]
    public LayerMask enemyLayer;

    private float nextShoveTime;

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && Time.time >= nextShoveTime)
        {
            Shove();
            nextShoveTime = Time.time + shoveCooldown;
        }
    }

    void Shove()
    {
        // Position the detection area in front of the player
        Vector3 shovePosition = transform.position + transform.forward * shoveRange;

        // Find enemies inside the shove area
        Collider[] enemies = Physics.OverlapSphere(shovePosition,1f,enemyLayer);

        foreach (Collider enemy in enemies)
        {
            Rigidbody enemyRb = enemy.GetComponent<Rigidbody>();

            if (enemyRb != null)
            {
                Vector3 shoveDirection = enemy.transform.position - transform.position;
                shoveDirection.y = 0f;
                shoveDirection.Normalize();

                enemyRb.AddForce(
                    shoveDirection * shoveForce,
                    ForceMode.Impulse
                );
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Vector3 shovePosition =
            transform.position + transform.forward * shoveRange;

        Gizmos.DrawWireSphere(shovePosition, 1f);
    }
}