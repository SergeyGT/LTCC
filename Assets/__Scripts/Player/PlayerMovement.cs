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
    
    private CharacterController _controller;
    private Vector3 _moveDirection;
    private PlayerInput _playerInput;
    private float _targetSpeed;

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
    }

    private void OnDisable()
    {
        _playerInput.Player.Sprint.performed -= StartSprint;
        _playerInput.Player.Sprint.canceled -= StopSprint;
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
    
    private void Update()
    {
        ReadMovement();
    }
    
    private void ReadMovement()
    {
        var directionInput = _playerInput.Player.Move.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(directionInput.x, 0f, directionInput.y);
        
        if (_cameraRotate != null)
            _moveDirection = Quaternion.Euler(0, _cameraRotate.eulerAngles.y, 0) * moveDirection;
        else
            _moveDirection = moveDirection;
    }
    
    private void UpdateSpeed()
    {
        _currentSpeed = Mathf.Lerp(_currentSpeed, _targetSpeed, _acceleration * Time.fixedDeltaTime);
        
        if (Mathf.Abs(_currentSpeed - _targetSpeed) < 0.01f)
            _currentSpeed = _targetSpeed;
    }
    
    private void FixedUpdate()
    {
        UpdateSpeed();
        Move();
    }
    
    private void Move()
    {
        Vector3 move = _moveDirection * _currentSpeed * Time.fixedDeltaTime;
        
        _controller.Move(move);
    }
}
