using UnityEngine;

public class MeleeHitbox : MonoBehaviour
{
    private int _damage;
    private float _duration;
    private string _targetTag;

    public void Initialize(int damage, float duration, string targetTag)
    {
        this._damage = damage;
        this._duration = duration;
        this._targetTag = targetTag;
        Destroy(gameObject, duration);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(_targetTag)) return;

        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(_damage);
        }
    }
}
