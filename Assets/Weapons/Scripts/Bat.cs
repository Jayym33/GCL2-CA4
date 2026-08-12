using UnityEngine;

public class Bat : MonoBehaviour
{
    [Header("Bat Stats")]
    public int damage = 50;
    public float attackCooldown = 0.8f;

    private float nextAttackTime = 0f;

    void Update()
    {
        // Left click to attack
        if (Input.GetMouseButtonDown(0) && Time.time >= nextAttackTime)
        {
            Attack();

            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void Attack()
    {
        Debug.Log("Bat swing!");

        // Tell the hitbox to activate
        BatHitbox hitbox = GetComponentInChildren<BatHitbox>();

        if (hitbox != null)
        {
            hitbox.ActivateHitbox();
        }
    }
}