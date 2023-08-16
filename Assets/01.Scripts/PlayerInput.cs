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

    private bool _uiMode = false;
    private void Awake()
    {
        _keyAction = new PlayerInputAction();
        _keyAction.PlayerInput.Enable();
        _keyAction.PlayerInput.Jump.performed += Jump;
        _keyAction.UI.Submit.performed += UISubmitPressed;

        _keyAction.PlayerInput.Disable();
        _keyAction.PlayerInput.Jump.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .WithCancelingThrough("<Keyboard>/escape")
            .OnComplete(op =>
            {
                Debug.Log($"변경 : {op.selectedControl}");
                op.Dispose();
                _keyAction.PlayerInput.Enable();
            })
            .OnCancel(op =>
            {
                Debug.Log("취소");
                op.Dispose();
                _keyAction.PlayerInput.Enable();
            })
            .Start();
        SaveKeyInfo();
    }

    private void SaveKeyInfo()
    {
        var json = _keyAction.SaveBindingOverridesAsJson();
        Debug.Log(json);

        PlayerPrefs.SetString("KeyInfo", json);
    }

    private void LoadKeyInfo()
    {
        var json = PlayerPrefs.GetString("keyInfo", null);
        if(json != null)
        {
            _keyAction.LoadBindingOverridesFromJson(json);
        }
    }

    private void UISubmitPressed(InputAction.CallbackContext obj)
    {
        Debug.Log("UI Submit 눌림");
    }

    private void Update()
    {
        _inputVector = _keyAction.PlayerInput.Movement.ReadValue<Vector2>();
        onMovement?.Invoke(_inputVector);

        if(Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadKeyInfo();
        }

        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            _keyAction.Disable();
            if(_uiMode == false)
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

    public void Jump(InputAction.CallbackContext context)
    {
        OnJump?.Invoke();
    }
}
