using UnityEngine;

public class MeleeHitbox : MonoBehaviour
{
    private int damage;
    private float duration;
    private string targetTag;

    public void Initialize(int damage, float duration, string targetTag)
    {
        this.damage = damage;
        this.duration = duration;
        this.targetTag = targetTag;
        Destroy(gameObject, duration);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(targetTag)) return;

        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }
    }
}
