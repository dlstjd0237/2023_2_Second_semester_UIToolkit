using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rig2D;
    //private PlayerInput _playerInput;
    private Controls _controls;
    private Controls.PlayerActions _playerAction;

    private void Awake()
    {
        _rig2D = GetComponent<Rigidbody2D>();

        _controls = new Controls();
        _playerAction = _controls.Player;
        _playerAction.Jump.performed += ctx => Jump();
        //_playerInput = GetComponent<PlayerInput>();

        //_playerInput.onActionTriggered += OnInputActionTriggered;

    }


    private void Jump()
    {
        Debug.Log("มกวม");
    }

    private void OnInputActionTriggered(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        Debug.Log(context.action.name);
    }

    //public void Jump(InputAction.CallbackContext context)
    //{
    //    if (context.performed)
    //    {
    //        _rig2D.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
    //    }

    //}

    private void OnEnable()
    {
        _playerAction.Enable();
    }
    private void OnDisable()
    {
        _playerAction.Disable();
    }
}
