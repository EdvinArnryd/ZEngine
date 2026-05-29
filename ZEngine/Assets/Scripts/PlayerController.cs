using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputSystem_Actions _inputActions;
    private Vector2 _moveInput;
    private Vector2 _rotateInput;
    private Rigidbody _rb;
    private Collider _col;

    public float _divider = 5;

    #region Player Controller Events
    public Action OnRunning;
    public Action OnBacking;
    public Action OnJumping;
    #endregion

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.75f, 0.0f, 0.0f, 0.75f);

        // Convert the local coordinate values into world
        // coordinates for the matrix transformation.
        _col = GetComponent<Collider>();
        Gizmos.DrawRay(transform.position + new Vector3(0,0.5f,0), Vector3.down * _divider);
    }

    void Awake()
    {
        _inputActions = new InputSystem_Actions();
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<Collider>();

        _inputActions.Player.Move.performed += OnMove;
        _inputActions.Player.Move.canceled += OnMove;
        _inputActions.Player.Jump.performed += OnJump;
        _inputActions.Player.Look.performed += OnLook;
        _inputActions.Player.Look.canceled += OnLook;
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
        PlayerLook();

        Debug.DrawRay(transform.position, Vector3.down, Color.blue);

        MovePlayer();

        print(GetIsGrounded());
    }

    private void MovePlayer()
    {
        Vector3 movement = new Vector3(_moveInput.x, 0, _moveInput.y);

        transform.Translate(movement * 5 * Time.deltaTime);
    }

    private void PlayerLook()
    {
        Vector2 rotation = _rotateInput * 0.2f;
        transform.Rotate(0,rotation.x, 0);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _rotateInput = context.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
        if(_moveInput.y < -0.5)
        {
            OnBacking.Invoke();
        }
        else if(_moveInput.y > 0.5 || _moveInput.x > 0.5 ||_moveInput.y < -0.5)
        {
            OnRunning.Invoke();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(GetIsGrounded())
        {
            _rb.AddForce(0,6,0, ForceMode.Impulse);
            OnJumping.Invoke();
        }
    }

    private bool GetIsGrounded()
    {
        return Physics.Raycast(
        transform.position + new Vector3(0,0.5f,0), 
        Vector3.down, 
        _divider,
        LayerMask.GetMask("Ground"));
    }

}
