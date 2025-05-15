using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerAttack : MonoBehaviour
{
    [Header("Weapon Data")]
    public WeaponData rangedWeapon;
    public WeaponData meleeWeapon;
    
    [SerializeField] private Transform _meleePoint;

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
        if (!canShoot || rangedWeapon == null || rangedWeapon.weaponType != WeaponType.Ranged)
            return;
        
        Vector2 mousePos = _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction  = (mousePos - (Vector2)transform.position).normalized;
        Vector2 spawnPos = (Vector2)transform.position + direction * rangedWeapon.projectileSpawnOffset;

        GameObject bullet = Instantiate(rangedWeapon.projectilePrefab,spawnPos, Quaternion.identity);
        bullet.GetComponent<Bullet>().Initialize(direction);

        StartCoroutine(ShootCooldownRoutine(rangedWeapon.cooldown));
    }
    
    private void OnAttackMelee(InputAction.CallbackContext context)
    {
        if (!canMelee || meleeWeapon == null || meleeWeapon.weaponType != WeaponType.Melee)
            return;
        
        Instantiate(meleeWeapon.meleeHitboxPrefab, _meleePoint.position, _meleePoint.rotation);

        StartCoroutine(MeleeCooldownRoutine(meleeWeapon.cooldown));
    }

    private IEnumerator ShootCooldownRoutine(float duration)
    {
        canShoot = false;
        yield return new WaitForSeconds(duration);
        canShoot = true;
    }

    private IEnumerator MeleeCooldownRoutine(float duration)
    {
        canMelee = false;
        yield return new WaitForSeconds(duration);
        canMelee = true;
    }
}
