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

        // Start the swing
        StartCoroutine(Swing());
    }

    System.Collections.IEnumerator Swing()
    {
        isSwinging = true;

        // Activate the hitbox
        BatHitbox hitbox = GetComponentInChildren<BatHitbox>();

        if (hitbox != null)
        {
            hitbox.ActivateHitbox();
        }

        float timer = 0f;

        // Swing from starting rotation to the side
        while (timer < swingDuration)
        {
            timer += Time.deltaTime;

            float progress = timer / swingDuration;

            // Smooth the movement
            float angle = Mathf.Lerp(0f, swingAngle, progress);

            transform.localRotation =
                startingRotation * Quaternion.Euler(0f, angle, 0f);

            yield return null;
        }

        // Return the bat to its starting position
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

        // Make absolutely sure we return to the original rotation
        transform.localRotation = startingRotation;

        isSwinging = false;
    }
}