
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerAttack : MonoBehaviour
{
    [Header("Fire Attack")]
    public GameObject bulletPrefab;
    [SerializeField] private float _bulletOffset = 0.5f;
    [SerializeField] private float _attackFireCooldown = 0.2f;

    [Header("Melee Attack")]
    public GameObject meleeHitboxPrefab;
    [SerializeField] private Transform _meleePoint;
    [SerializeField] private float _attackMeleeCooldown = 0.5f;

    private bool canShoot = true;
    private bool canMelee = true;
    
    private Camera _mainCamera;
    private PlayerInputAction _inputAction;

    private void Awake()
    {
        _inputAction = new PlayerInputAction();
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        _inputAction.Player.Enable();
        _inputAction.Player.AttackFire.performed += OnAttackFire;
        _inputAction.Player.AttackMelee.performed += OnAttackMelee;
    }

    private void OnDisable()
    {
        _inputAction.Player.AttackFire.performed -= OnAttackFire; 
        _inputAction.Player.AttackMelee.performed -= OnAttackMelee; 
        _inputAction.Player.Disable();
    }

    private void OnAttackFire(InputAction.CallbackContext context)
    {
        if (!canShoot) return;
        
        Vector2 mousePos = _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction  = (mousePos - (Vector2)transform.position).normalized;
        Vector2 spawnPos = (Vector2)transform.position + direction * _bulletOffset;
        

        GameObject bullet = Instantiate(bulletPrefab,spawnPos, Quaternion.identity);
        bullet.GetComponent<Bullet>().Initialize(direction);

        canShoot = false; 
        Invoke(nameof(ResetFireAttack), _attackFireCooldown);
    }
    
    private void OnAttackMelee(InputAction.CallbackContext context)
    {
        if (!canMelee) return;

        GameObject hitbox = Instantiate(meleeHitboxPrefab, _meleePoint.position, _meleePoint.rotation);
        canMelee = false;
        Invoke(nameof(ResetMeleeAttack), _attackMeleeCooldown);
    }
    
    private void ResetFireAttack() => canShoot = true;
    private void ResetMeleeAttack() => canMelee = true;
    
}
