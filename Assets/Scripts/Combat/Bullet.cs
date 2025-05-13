using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10f;
    [SerializeField]
    private float _lifeTime = 3f;
    [SerializeField]
    private int _damage = 1;

    private Vector2 _direction;

    public void Initialize(Vector2 dir)
    {
        _direction = dir.normalized;
        Destroy(gameObject, _lifeTime);
    }

    private void Update()
    {
        transform.Translate(_direction * (_speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(_damage);
            }
        }
        
        Destroy(gameObject);
    }
}
