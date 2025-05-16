using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 direction;
    private float speed;
    private float lifetime;
    private int damage;
    private string targetTag;

    public void Initialize(Vector2 dir, float speed, float lifetime, int damage, string targetTag)
    {
        this.direction = dir.normalized;
        this.speed = speed;
        this.lifetime = lifetime;
        this.damage = damage;
        this.targetTag = targetTag;

        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.Translate(direction * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(targetTag)) return;

        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}