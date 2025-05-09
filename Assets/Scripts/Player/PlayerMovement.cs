using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   private PlayerInputAction _inputActions;
   private Vector2 _moveInput;
   [SerializeField] private float _moveSpeed = 5f;
   private Rigidbody2D _rb;

   private void Awake()
   {
      _inputActions = new PlayerInputAction();
      _rb = GetComponent<Rigidbody2D>(); //add check ensure RB
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
   }
}
