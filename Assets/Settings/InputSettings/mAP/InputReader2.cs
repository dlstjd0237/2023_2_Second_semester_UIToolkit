using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

[CreateAssetMenu(menuName = "SO/Player2")]
public class InputReader2 : ScriptableObject, Controls2.IPlayerActions
{
    public event Action<Vector2> MovementEvent;
    public event Action ChangeEvent;

    private Controls2 _controls;

    private void OnEnable()
    {
        if(_controls == null)
        {
            _controls = new Controls2();
            _controls.Player.SetCallbacks(this);
        }

        _controls.Player.Enable();
    }
    public void OnChange(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ChangeEvent?.Invoke();
        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();
        MovementEvent?.Invoke(movement);
    }
}
