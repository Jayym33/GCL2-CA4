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
        Debug.Log("Hit " + collision.gameObject.name);

        Destroy(gameObject);
    }
}