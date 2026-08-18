using UnityEngine;

public class Bat : MonoBehaviour
{
    [Header("Bat Stats")]
    public int damage = 50;
    public float attackCooldown = 0.8f;

    [Header("Swing Settings")]
    public float swingAngle = 100f;
    public float swingDuration = 0.2f;

    private float nextAttackTime = 0f;
    private bool isSwinging = false;

    private Quaternion startingRotation;

    void Start()
    {
        // Remember the bat's starting rotation
        startingRotation = transform.localRotation;
    }

    void Update()
    {
        // Do nothing if this bat is not currently active
        if (!gameObject.activeInHierarchy)
            return;

        // Left click to swing
        if (Input.GetMouseButtonDown(0) &&
            Time.time >= nextAttackTime &&
            !isSwinging)
        {
            Attack();
        }
    }

    void Attack()
    {
        nextAttackTime = Time.time + attackCooldown;

        StartCoroutine(Swing());
    }

    System.Collections.IEnumerator Swing()
    {
        isSwinging = true;

        // Activate hitbox
        BatHitbox hitbox = GetComponentInChildren<BatHitbox>();

        if (hitbox != null)
        {
            hitbox.ActivateHitbox();
        }

        // Swing forward
        float timer = 0f;

        while (timer < swingDuration)
        {
            timer += Time.deltaTime;

            float progress = timer / swingDuration;

            float angle = Mathf.Lerp(0f, swingAngle, progress);

            transform.localRotation =
                startingRotation * Quaternion.Euler(0f, angle, 0f);

            yield return null;
        }

        // Return to starting rotation
        timer = 0f;

        while (timer < swingDuration)
        {
            timer += Time.deltaTime;

            float progress = timer / swingDuration;

            float angle = Mathf.Lerp(swingAngle, 0f, progress);

            transform.localRotation =
                startingRotation * Quaternion.Euler(0f, angle, 0f);

            yield return null;
        }

        // Make sure the bat returns exactly to its original rotation
        transform.localRotation = startingRotation;

        isSwinging = false;
    }
}