using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 _direction;
    private float _speed;
    private float _lifetime;
    private int _damage;
    private string _targetTag;

    public void Initialize(Vector2 dir, float speed, float lifetime, int damage, string targetTag)
    {
        this._direction = dir.normalized;
        this._speed = speed;
        this._lifetime = lifetime;
        this._damage = damage;
        this._targetTag = targetTag;

        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.Translate(_direction * (_speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(_targetTag)) return;

        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(_damage);
        }

        Destroy(gameObject);
    }
}