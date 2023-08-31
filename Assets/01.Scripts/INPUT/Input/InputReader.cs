using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controlls;

[CreateAssetMenu(menuName = "SO/INPUTREADER")]
public class InputReader : ScriptableObject, IPlayerActions
{
    public event Action<Vector2> MovementEvent;
    public Vector2 AimPosition { get; private set; }
    private Controlls _controls;

    private void OnEnable()
    {
        if(_controls == null)
        {
            _controls = new Controlls();
            _controls.Player.SetCallbacks(this);
        }

        _controls.Player.Enable();
    }
    public void OnAIM(InputAction.CallbackContext context)
    {
        AimPosition = context.ReadValue<Vector2>();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        MovementEvent?.Invoke(value);
    }
}
