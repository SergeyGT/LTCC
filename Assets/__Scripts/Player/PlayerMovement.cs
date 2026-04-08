using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speedWalk;
    [SerializeField] private float _speedRun;
    [SerializeField] private Transform _cameraRotate;
    [SerializeField] private float _currentSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _gravityForce;
    [SerializeField] private float _jumpForce;
    
    private CharacterController _controller;
    private Vector3 _moveDirection;
    private PlayerInput _playerInput;
    private float _targetSpeed;
    private float _verticalVelocity;
    
    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Player.Enable();
        _playerInput.UI.Enable();
        _controller = GetComponent<CharacterController>();
        _currentSpeed = _speedWalk;
        _targetSpeed = _speedWalk;
    }

    private void OnEnable()
    {
        _playerInput.Player.Sprint.performed += StartSprint;
        _playerInput.Player.Sprint.canceled += StopSprint;

        _playerInput.Player.Jump.performed += Jump;
    }

    private void OnDisable()
    {
        _playerInput.Player.Sprint.performed -= StartSprint;
        _playerInput.Player.Sprint.canceled -= StopSprint;
        
        _playerInput.Player.Jump.performed -= Jump;
        
        _playerInput.Disable();
    }

    private void StartSprint(InputAction.CallbackContext ctx)
    {
        _targetSpeed = _speedRun;
    }

    private void StopSprint(InputAction.CallbackContext ctx)
    {
        _targetSpeed = _speedWalk;
    }

    private void Jump(InputAction.CallbackContext ctx)
    {
       if(_controller.isGrounded) _verticalVelocity = _jumpForce;
    } 
    
    private void Update() => ReadMovement();
    
    private void ReadMovement()
    {
        var directionInput = _playerInput.Player.Move.ReadValue<Vector2>();

        _verticalVelocity += _gravityForce * Time.fixedDeltaTime;
        
        Vector3 moveDirection = new Vector3(directionInput.x, _verticalVelocity, directionInput.y);
        
        if (_cameraRotate != null)
            _moveDirection = Quaternion.Euler(0, _cameraRotate.eulerAngles.y, 0) * moveDirection;
        else
            _moveDirection = moveDirection;
    }
    
    private void UpdateSpeed()
    {
        if (!IsMoving()) return;
        
        _currentSpeed = Mathf.Lerp(_currentSpeed, _targetSpeed, _acceleration * Time.fixedDeltaTime);
        
        if (Mathf.Abs(_currentSpeed - _targetSpeed) < 0.01f)
            _currentSpeed = _targetSpeed;
    }
    

    private void Rotate()
    {
        var rotateBody = new Vector3(0, _cameraRotate.eulerAngles.y, 0);
        transform.rotation = Quaternion.Euler(rotateBody);
    }
    
    private void Move()
    {
        Vector3 move = _moveDirection * _currentSpeed * Time.fixedDeltaTime;
        
        _controller.Move(move);
    }

    private bool IsMoving()
    {
        var move = _playerInput.Player.Move.ReadValue<Vector2>();
        return move.magnitude > 0.2f;
    }
    private void FixedUpdate()
    {
        UpdateSpeed();
        Move();
        Rotate();
    }
}
