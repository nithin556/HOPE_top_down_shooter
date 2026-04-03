using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    NewPlayerInputAction inputActions;
    private Vector2 rawInputVector;

    void Awake()
    {
        inputActions = new NewPlayerInputAction();
    }

    void OnEnable()
    {
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Enable();
        inputActions.Player.Move.canceled += OnMove;
    }

    void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Disable();
    }

    void OnMove(InputAction.CallbackContext callbackContext)
    {
        rawInputVector = callbackContext.ReadValue<Vector2>();
    }

    public Vector2 GetMovementVector()
    {
        return rawInputVector;
    }
}
