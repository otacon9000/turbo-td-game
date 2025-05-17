using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerAttack : MonoBehaviour
{
    [Header("Weapon Data")]
    public WeaponData rangedWeapon;
    public WeaponData meleeWeapon;
    
    [SerializeField] private Transform _meleePoint;
    
    private Camera _mainCamera;
    private PlayerInputAction _inputAction;
    private MeleeAttackHandler _meleeHandler;
    private RangedAttackHandler _rangedHandler;

    private void Awake()
    {
        _inputAction = new PlayerInputAction();
        _mainCamera = Camera.main;
        _meleeHandler = new MeleeAttackHandler(_meleePoint, meleeWeapon, this);
        _rangedHandler = new RangedAttackHandler(transform, rangedWeapon, this);
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
        // if (!canShoot || rangedWeapon == null || rangedWeapon.weaponType != WeaponType.Ranged)
        //     return;
        
        Vector2 mousePosition = _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        if (_rangedHandler.CanAttack)
        {
            _rangedHandler.Execute(mousePosition);
        }
    }
    
    private void OnAttackMelee(InputAction.CallbackContext context)
    {
        Vector2 mousePosition = _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        if (_meleeHandler.CanAttack)
        {
            _meleeHandler.Execute(mousePosition);
        }
    }
}
