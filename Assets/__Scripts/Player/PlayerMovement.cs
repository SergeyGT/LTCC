using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    private CharacterController _controller;
    private Vector3 _moveDirection;
    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Player.Enable();
        _playerInput.UI.Enable();
        _controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }


    private void Update()
    {
        ReadMovement();
    }

    private void ReadMovement()
    {
        var directionInput = _playerInput.Player.Move.ReadValue<Vector2>();
        _moveDirection = new Vector3(directionInput.x, 0f, directionInput.y);
    }

    private void FixedUpdate()
    {
        _controller.Move(_moveDirection * _speed * Time.fixedDeltaTime);
    }
}
