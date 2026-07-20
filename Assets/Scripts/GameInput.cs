using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    NewPlayerInputAction inputActions;
    private Vector2 rawInputVector;
    private bool isFiring;

    void Awake()
    {
        inputActions = new NewPlayerInputAction();
    }

    void OnEnable()
    {
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Fire.started += OnFire;

        inputActions.Player.Enable();
        inputActions.Player.Move.canceled += OnMove;
        inputActions.Player.Fire.canceled += OnFireCancel;
    }

    void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Fire.started -= OnFire;
        inputActions.Player.Fire.started -= OnFireCancel;
        inputActions.Player.Disable();
    }

    void OnMove(InputAction.CallbackContext callbackContext)
    {
        rawInputVector = callbackContext.ReadValue<Vector2>();
    }

    void OnFire(InputAction.CallbackContext callbackContext)
    {
        isFiring = true;
    }
    void OnFireCancel(InputAction.CallbackContext callbackContext)
    {
        isFiring = false;
    }

    public Vector2 GetMovementVector()
    {
        return rawInputVector;
    }

    public bool GetIsFiring()
    {
        return isFiring;
    }

}
