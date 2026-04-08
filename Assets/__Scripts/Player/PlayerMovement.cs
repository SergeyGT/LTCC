using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speedWalk;
    [SerializeField] private float _speedRun;
    [SerializeField] private Transform _cameraRotate;
    private CharacterController _controller;
    private Vector3 _moveDirection;
    private PlayerInput _playerInput;
    private float _currentSpeed;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Player.Enable();
        _playerInput.UI.Enable();
        _controller = GetComponent<CharacterController>();
        _currentSpeed = _speedWalk;
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
    }

    private void StartSprint(InputAction.CallbackContext ctx)
    {
        _currentSpeed = _speedRun;
    }

    private void StopSprint(InputAction.CallbackContext ctx)
    {
        _currentSpeed = _speedWalk;
    }
    
    private void Update()
    {
        ReadMovement();
    }

    private void ReadMovement()
    {
        var directionInput = _playerInput.Player.Move.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(directionInput.x, 0f, directionInput.y);

        _moveDirection = Quaternion.Euler(0, _cameraRotate.eulerAngles.y, 0) * moveDirection;
    }

    private void FixedUpdate()
    {
        _controller.Move(_moveDirection * _currentSpeed * Time.fixedDeltaTime);
    }
}
