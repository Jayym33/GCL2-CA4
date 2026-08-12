using UnityEngine;

public class BatHitbox : MonoBehaviour
{
    public int damage = 50;

    private bool canHit = false;

    public void ActivateHitbox()
    {
        canHit = true;

        // Turn hit detection off shortly after the swing
        Invoke(nameof(DeactivateHitbox), 0.2f);
    }

    private void DeactivateHitbox()
    {
        canHit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canHit)
            return;

        BasicEnemyController enemy =
            other.GetComponent<BasicEnemyController>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);

            Debug.Log("Bat hit enemy!");

            // Prevent hitting the same enemy repeatedly
            canHit = false;
        }
    }
}