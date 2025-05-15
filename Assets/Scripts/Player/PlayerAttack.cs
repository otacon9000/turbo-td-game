
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerAttack : MonoBehaviour
{
    [Header("Fire Attack")]
    public GameObject bulletPrefab;
    [SerializeField] private Transform _bulletSpawn;
    [SerializeField] private float _attackFireCooldown = 0.2f;

    [Header("Melee Attack")]
    public GameObject meleeHitboxPrefab;
    [SerializeField] private Transform _meleePoint;
    [SerializeField] private float _attackMeleeCooldown = 0.5f;

    private bool canAttack = true;
    
    private Camera _mainCamera;
    private float _lastAttackTime;
    private PlayerInputAction _inputAction;

    private void Awake()
    {
        _inputAction = new PlayerInputAction();
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        _inputAction.Player.Enable();
        _inputAction.Player.Attack.performed += OnAttackFire;
        _inputAction.Player.Attack.performed += OnAttackMelee;
    }

    private void OnDisable()
    {
        _inputAction.Player.Attack.performed -= OnAttackFire; 
        _inputAction.Player.Attack.performed -= OnAttackMelee; 
        _inputAction.Player.Disable();
    }

    private void OnAttackFire(InputAction.CallbackContext context)
    {
        if (Time.time - _lastAttackTime < _attackFireCooldown) return;
        
        Vector2 mousePoint = _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 shootDirection = mousePoint - (Vector2)_bulletSpawn.position;
        

        GameObject bullet = Instantiate(bulletPrefab, _bulletSpawn.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Initialize(shootDirection);

        _lastAttackTime = Time.time;
    }
    
    private void OnAttackMelee(InputAction.CallbackContext context)
    {
        if (!canAttack) return;

        GameObject hitbox = Instantiate(meleeHitboxPrefab, _meleePoint.position, _meleePoint.rotation);
        canAttack = false;
        Invoke(nameof(ResetAttack), _attackMeleeCooldown);
    }
    
    private void ResetAttack()
    {
        canAttack = true;
    }
    
}
