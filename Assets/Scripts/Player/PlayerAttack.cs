using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bulletPrefab;
    [SerializeField] private Transform _bulletSpawn;
    [SerializeField] private float _attackCooldown = 0.2f;

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
        _inputAction.Player.Attack.performed += OnAttack;
    }

    private void OnDisable()
    {
        _inputAction.Player.Attack.performed -= OnAttack; 
        _inputAction.Player.Disable();
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (Time.time - _lastAttackTime < _attackCooldown) return;
        
        Vector2 mousePoint = _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 shootDirection = mousePoint - (Vector2)_bulletSpawn.position;

        GameObject bullet = Instantiate(bulletPrefab, _bulletSpawn.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Initialize(shootDirection);

        _lastAttackTime = Time.time;
    }
}
