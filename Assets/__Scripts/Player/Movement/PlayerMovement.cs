using System;
using System.Collections.Generic;
using __Scripts.Player;
using NUnit.Framework;
using Unity.VisualScripting;
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
    [SerializeField] private float _senseMoving;
    [SerializeField] private float _rotationSpeed = 540f;
    
    private CharacterController _controller;
    private IPlayerMovement _currentMovement;
    private Vector3 _moveDirection;
    private PlayerInput _playerInput;
    private float _targetSpeed;
    private Quaternion _targetRotation;
    private float _verticalVelocity;
    
    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Player.Enable();
        _playerInput.UI.Enable();
        _controller = GetComponent<CharacterController>();
        _currentSpeed = _speedWalk;
        _targetSpeed = _speedWalk;
        _currentMovement = gameObject.AddComponent<FirstPersonMovement>();
    }

    
    private void OnEnable()
    {
        _playerInput.Player.Sprint.performed += StartSprint;
        _playerInput.Player.Sprint.canceled += StopSprint;

        _playerInput.Player.Jump.performed += Jump;

        ChangerMovement.changeMovement += SwitchMovement;
        CameraViewChanger.changeCameraRotate += ChangeCamera;
    }

    private void OnDisable()
    {
        _playerInput.Player.Sprint.performed -= StartSprint;
        _playerInput.Player.Sprint.canceled -= StopSprint;
        
        _playerInput.Player.Jump.performed -= Jump;
        
        ChangerMovement.changeMovement -= SwitchMovement;
        CameraViewChanger.changeCameraRotate -= ChangeCamera;
        
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
    
    private void RotateCharacter()
    {
        if (_moveDirection.magnitude > 0.1f)
        {
            _targetRotation = _currentMovement.Rotation(_cameraRotate);
        }
        
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation, 
            _targetRotation, 
            _rotationSpeed * Time.fixedDeltaTime 
        );
    }
    private void SwitchMovement(IPlayerMovement newMovement)
    {
        _currentMovement = newMovement;
    }

    private void ChangeCamera(Transform newCamera)
    {
        _cameraRotate =  newCamera;
    }

    private void Update() => ReadMove();

    
    private void ReadMove()
    {
        Vector2 input =  _playerInput.Player.Move.ReadValue<Vector2>();
        
        if (!_controller.isGrounded)
            _verticalVelocity += _gravityForce * Time.fixedDeltaTime;
        _moveDirection = _currentMovement.ReadMovement(
                input, _gravityForce, _verticalVelocity, _cameraRotate);

    }
    
    private void UpdateSpeed()
    {
        if (!IsMoving())
        {
            _currentSpeed = _speedWalk;
            return;
        }
        
        _currentSpeed = Mathf.Lerp(_currentSpeed, _targetSpeed, _acceleration * Time.fixedDeltaTime);
        
        if (Mathf.Abs(_currentSpeed - _targetSpeed) < 0.01f)
            _currentSpeed = _targetSpeed;
    }
    
    private void Move()
    {
        Vector3 horizontal = new Vector3(_moveDirection.x, 0, _moveDirection.z) * _currentSpeed;

        Vector3 vertical = new Vector3(0, _moveDirection.y, 0);

        Vector3 finalMove = (horizontal + vertical) * Time.fixedDeltaTime;

        _controller.Move(finalMove);
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
        RotateCharacter();
    }
}
