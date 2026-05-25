using UnityEditor.Animations;
using UnityEngine;

public enum PlayerState
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
    private PlayerState _newState;
    private PlayerState _oldState;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        _newState = PlayerState.IDLE;
        _oldState = PlayerState.NONE;
    }

    void Update()
    { 
        UpdateAnimations();
    }

    private void UpdateAnimations()
    {
        if(_newState == _oldState) return;
        print("Going into Animations");
        _oldState = _newState;
        switch(_newState)
        {
            case PlayerState.IDLE:
                _animator.CrossFadeInFixedTime("Idle", 0.25f);
                break;
            case PlayerState.RUNNING:
                _animator.CrossFadeInFixedTime("Running", 0.25f);
                break;
            case PlayerState.JUMPING:
                _animator.CrossFadeInFixedTime("Jumping", 0.25f);
                break;
            case PlayerState.FALLING:
                _animator.CrossFadeInFixedTime("Falling", 0.25f);
                break;
            case PlayerState.LANDING:
                _animator.CrossFadeInFixedTime("Landing", 0.25f);
                break;
            case PlayerState.BACKING:
                _animator.CrossFadeInFixedTime("Backing", 0.25f);
                break;
        }
    }
}
