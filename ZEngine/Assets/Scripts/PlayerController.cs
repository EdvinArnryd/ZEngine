using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputSystem_Actions _inputActions;

    private Vector2 _moveInput;
    private Rigidbody rb;

    void Awake()
    {
        _inputActions = new InputSystem_Actions();
        rb = GetComponent<Rigidbody>();

        _inputActions.Player.Move.performed += OnMove;
        _inputActions.Player.Move.canceled += OnMove;
        _inputActions.Player.Jump.performed += OnJump;
        _inputActions.Player.Jump.canceled += OnJump;
    }

    private void OnEnable()
    {
        _inputActions.Player.Enable();        
    }

    private void OnDisable()
    {
        _inputActions.Player.Disable();       
    }

    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 movement = new Vector3(_moveInput.x, 0, _moveInput.y);

        transform.Translate(movement * 5 * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        rb.AddForce(0,4,0, ForceMode.Impulse);
    }
}
