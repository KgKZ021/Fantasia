using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// To handle the new input system
/// </summary>
public class GameInput : MonoBehaviour
{
    private PlayerInputAction playerActions;

    public event EventHandler OnCollectWeapon;

    private void Awake()
    {
        playerActions = new PlayerInputAction();
        playerActions.Player.Enable();

        playerActions.Player.Collect.performed += Collect_performed;

    }

    private void Collect_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnCollectWeapon?.Invoke(this,EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
}
