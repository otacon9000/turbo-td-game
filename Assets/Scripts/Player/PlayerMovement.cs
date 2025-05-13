using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
   private PlayerInputAction _inputActions;
   private Vector2 _moveInput;
   [SerializeField] private float _moveSpeed = 5f;
   private Rigidbody2D _rb;

   private Camera _mainCamera;

   private void Awake()
   {
      _inputActions = new PlayerInputAction();
      _rb = GetComponent<Rigidbody2D>(); //add check ensure RB
      _mainCamera = Camera.main;
   }

   private void OnEnable()
   {
      _inputActions.Player.Enable();
      _inputActions.Player.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
      _inputActions.Player.Move.canceled += ctx => _moveInput = Vector2.zero;
   }
   private void OnDisable()
   {
      _inputActions.Player.Disable();
   }

   private void FixedUpdate()
   {
      _rb.velocity = _moveInput * _moveSpeed;
      PlayerAim();
   }

   private void PlayerAim()
   {
      Vector3 mouseWorldPos = _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
      mouseWorldPos.z = 0f;

      Vector3 direction = (mouseWorldPos - transform.position).normalized;

      float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

      transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
   }
}
