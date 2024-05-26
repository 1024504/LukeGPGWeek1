using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    private PlayerControls _inputActions;

    private InputAction _move;

    public Agent agent;

    private void OnEnable()
    {
	    _inputActions = new PlayerControls();
		_move = _inputActions.Player.Move;
		_move.performed += MovePerformed;
		_move.canceled += MoveCancelled;
		_move.Enable();
    }
    
    private void OnDisable()
	{
	    _move.Disable();
	    _move.performed -= MovePerformed;
	    _move.canceled -= MoveCancelled;
	    _inputActions.Dispose();
	}
    
    private void MovePerformed(InputAction.CallbackContext context)
	{
	    agent.MovePerformed(context.ReadValue<Vector2>());
	}

    private void MoveCancelled(InputAction.CallbackContext context)
    {
	    agent.MoveCancelled();
    }
}
