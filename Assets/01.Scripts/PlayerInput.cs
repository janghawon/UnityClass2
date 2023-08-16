using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInput : MonoBehaviour
{
    private PlayerInputAction _keyAction;

    public Action<Vector2> OnMovement;
    public Action OnJump;

    private bool _uiMode = false;

    private void Awake()
    {
        _keyAction = new PlayerInputAction();
        LoadKeyInfo();
        _keyAction.PlayerInput.Enable();
        _keyAction.PlayerInput.Jump.performed += Jump;

        _keyAction.UI.Submit.performed += UISubmitPressed;

        _keyAction.PlayerInput.Disable();
        _keyAction.PlayerInput.Jump.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .WithCancelingThrough("<keyboard>/escape")
            .OnComplete(op =>
            {
                Debug.Log(op.selectedControl);
                op.Dispose();
                _keyAction.PlayerInput.Enable();
                SaveKeyInfo();
            })
            .OnCancel(op =>
            {
                Debug.Log("Cancel");
                op.Dispose();
                _keyAction.PlayerInput.Enable();
            })
            .Start();
    }

    private void SaveKeyInfo()
    {
        string json = _keyAction.SaveBindingOverridesAsJson();
        Debug.Log(json);

        PlayerPrefs.SetString("keyInfo", json);
    }

    private void LoadKeyInfo()
    {
        string json = PlayerPrefs.GetString("keyInfo", null);
        if(json != null)
        {
            _keyAction.LoadBindingOverridesFromJson(json);
        }
    }

    private void UISubmitPressed(CallbackContext context)
    {
        Debug.Log("Submit");
    }

    private void Update()
    {
        Vector2 inputVector = _keyAction.PlayerInput.Movement.ReadValue<Vector2>();
        OnMovement?.Invoke(inputVector);

        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            _keyAction.Disable();
            if(!_uiMode)
            {
                _keyAction.UI.Enable();
            }
            else
            {
                _keyAction.PlayerInput.Enable();
            }
            _uiMode = !_uiMode;
        }
    }

    public void Jump(CallbackContext context)
    {
        OnJump?.Invoke();
    }
}
