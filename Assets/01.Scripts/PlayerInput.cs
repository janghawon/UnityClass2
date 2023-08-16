using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private PlayerInputAction _keyAction;
    [SerializeField] private float _speed = 20f;
    private Vector2 _inputVector;

    public Action<Vector2> onMovement;
    public Action OnJump;

    private void Awake()
    {
        _keyAction = new PlayerInputAction();
        _keyAction.PlayerInput.Enable();
        _keyAction.PlayerInput.Jump.performed += Jump;
    }

    private void Update()
    {
        _inputVector = _keyAction.PlayerInput.Movement.ReadValue<Vector2>();
        onMovement?.Invoke(_inputVector);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        OnJump?.Invoke();
    }
}
