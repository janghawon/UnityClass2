using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputlogic : MonoBehaviour
{
    public PlayerInputAction keyAction;

    public event Action<Vector2> OnMovement;
    public event Action<Vector2> OnAim;
    public event Action OnJump;
    public event Action OnFire;

    private void Awake()
    {
        keyAction = new PlayerInputAction();
        LoadKeyInfo();
        keyAction.Player.Enable();
        keyAction.Player.Jump.performed += Jump;
        keyAction.Player.Fire.performed += FireHandle;
    }

    private void LoadKeyInfo()
    {
        string json = PlayerPrefs.GetString("keyInfo", null);
        if(json != null)
        {
            keyAction.LoadBindingOverridesFromJson(json);
        }
    }

    private void Update()
    {
        Vector2 inputVector = keyAction.Player.Movement.ReadValue<Vector2>();
        OnMovement?.Invoke(inputVector);
        Vector3 dir = keyAction.Player.Aim.ReadValue<Vector3>();
        OnAim?.Invoke(dir);
    }

    public void Jump(CallbackContext context)
    {
        OnJump?.Invoke();
    }

    private void FireHandle(CallbackContext context)
    {
        OnFire?.Invoke();
    }

}
