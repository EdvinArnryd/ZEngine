using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputSystem_Actions inputActions;

    void Awake()
    {
        inputActions = new InputSystem_Actions();

        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Jump.performed += OnJump;
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();        
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();       
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("Player Moved!");
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("Player Jumped!");
    }
}
