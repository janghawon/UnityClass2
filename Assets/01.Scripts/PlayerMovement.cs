using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _gravity = -9.8f;

    private float _gravityMultiplier = 1f;
    [SerializeField] private float _jumpPower = 4f;

    private CharacterController _characterController;
    public bool IsGround => _characterController.isGrounded;

    private Vector2 _inputDirection;
    private Vector3 _movementVelocity;
    public Vector3 MovementVelocity => _movementVelocity;
    private float _verticalVelocity;

    private bool _activeMove = true;
    public bool ActiveMove
    {
        get => _activeMove;
        set => _activeMove = value;
    }

    private PlayerInput _playerInput;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.OnMovement += SetPlayerMovement;
        _playerInput.OnJump += Jump;
    }

    private void Jump()
    {
        if (!IsGround) return;
        _verticalVelocity += _jumpPower;
    }

    private void CalculatePlayerMovement()
    {
        _movementVelocity = new Vector3(_inputDirection.x, 0, _inputDirection.y) * (_moveSpeed * Time.fixedDeltaTime);

        if(_movementVelocity.sqrMagnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(_movementVelocity);
        }
    }

    public void StopImmediately()
    {
        _movementVelocity = Vector3.zero;
    }

    public void SetMovement(Vector3 value)
    {
        _movementVelocity = value;
        _movementVelocity.y = 0;
        _verticalVelocity = value.y;
    }

    private void ApplyGravity()
    {
        if(IsGround && _verticalVelocity < 0)
        {
            _verticalVelocity = -1f;
        }
        else
        {
            _verticalVelocity += _gravity * _gravityMultiplier * Time.fixedDeltaTime;
        }
        _movementVelocity.y = _verticalVelocity;
    }

    private void Move()
    {
        _characterController.Move(_movementVelocity);
    }

    private void FixedUpdate()
    {
        if(_activeMove)
        {
            CalculatePlayerMovement();
        }
        ApplyGravity();
        Move();
    }

    private void SetPlayerMovement(Vector2 value)
    {
        _inputDirection = value;
    }
}
