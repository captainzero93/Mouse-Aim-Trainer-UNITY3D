using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Target2D target = collision.GetComponent<Target2D>();
        if (target != null)
        {
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}