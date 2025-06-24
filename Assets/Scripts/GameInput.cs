using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    private PlayerInputAction playerActions;

    public event EventHandler OnCollectWeapon;

    [HideInInspector] public float lastHorizontalVector;
    [HideInInspector] public float lastVerticalVector;
    [HideInInspector] public Vector2 lastMovedVector;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        playerActions = new PlayerInputAction();
        playerActions.Player.Enable();

        playerActions.Player.Collect.performed += Collect_performed;
    }

    private void Start()
    {
        lastMovedVector = new Vector2(1, 0f);
    }

    private void Update()
    {
        LastMovedVector();
    }

    private void Collect_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnCollectWeapon?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerActions.Player.Move.ReadValue<Vector2>();
        return inputVector.normalized;
    }

    public void LastMovedVector()
    {
        Vector2 inputvector = GetMovementVectorNormalized();

        if (inputvector.x != 0)
        {
            lastHorizontalVector = inputvector.x;
            lastMovedVector = new Vector2(lastHorizontalVector, 0f);
        }
        if (inputvector.y != 0)
        {
            lastVerticalVector = inputvector.y;
            lastMovedVector = new Vector2(0f, lastVerticalVector);
        }
        if (inputvector.x != 0 && inputvector.y != 0)
        {
            lastMovedVector = new Vector2(lastHorizontalVector, lastVerticalVector);
        }
    }
}
