using UnityEngine;

public class MeleeHitbox : MonoBehaviour
{
    
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _duration = 0.2f;
    [SerializeField] private string _targetTag = "Enemy";

    private void Start()
    {
        Destroy(gameObject, _duration);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(_targetTag)) return;

        IDamageable target = collision.GetComponent<IDamageable>();
        if (target != null)
        {
            target.TakeDamage(_damage);
        }
    }
}