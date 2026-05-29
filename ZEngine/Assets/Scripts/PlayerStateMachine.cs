using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.TextCore.Text;

public enum State
{
    IDLE,
    WALKING,
    RUNNING,
    JUMPING,
    FALLING,
    LANDING,
    BACKING,
    NONE
}

public class PlayerStateMachine : MonoBehaviour
{
    private Animator _animator; 
    private State _currentState;
    private State _oldState;
    private PlayerController _playerController;


    void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
    }

    void Start()
    {
        _currentState = State.IDLE;
        _oldState = State.NONE;

        _playerController.OnRunning = SetRunning;
        _playerController.OnBacking = SetBacking;
        _playerController.OnJumping = SetJumping;
    }

    void Update()
    { 
        UpdateAnimations();
    }

    private void SetRunning()
    {
        _currentState = State.RUNNING;
    }

    private void SetBacking()
    {
        _currentState = State.BACKING;
    }

    private void SetIdle()
    {
        _currentState = State.IDLE;
    }

    private void SetJumping()
    {
        _currentState = State.JUMPING;
    }

    private void SetFalling()
    {
        
    }

    private void SetLanding()
    {
        
    }

    private void UpdateAnimations()
    {
        if(_currentState == _oldState) return;
        _oldState = _currentState;
        
        switch(_currentState)
        {
            case State.IDLE:
                _animator.CrossFadeInFixedTime("Idle", 0.25f);
                break;
            case State.RUNNING:
                _animator.CrossFadeInFixedTime("Run", 0.25f);
                break;
            case State.JUMPING:
                _animator.CrossFadeInFixedTime("Jump", 0.25f);
                break;
            case State.FALLING:
                _animator.CrossFadeInFixedTime("Falling", 0.25f);
                break;
            case State.LANDING:
                _animator.CrossFadeInFixedTime("Landing", 0.25f);
                break;
            case State.BACKING:
                _animator.CrossFadeInFixedTime("Backing", 0.25f);
                break;
        }
    }
}
