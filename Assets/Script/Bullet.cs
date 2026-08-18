using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 20;
    public float destroyAfter = 5f;

    void Start()
    {
        Destroy(gameObject, destroyAfter);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Bullet hit: " + collision.gameObject.name);

        BasicEnemyController enemy =
            collision.gameObject.GetComponent<BasicEnemyController>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}