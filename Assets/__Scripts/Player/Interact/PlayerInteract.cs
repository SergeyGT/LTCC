using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Zenject;

namespace __Scripts.Player.Interact
{
    public class PlayerInteract :  MonoBehaviour
    {
        public static event UnityAction<CinemachineCamera> changeCamera;
    
        [Header("Камеры на которые произвести переключения")]
        [SerializeField] private CinemachineCamera[] _virtualCameras;
        [Inject] private PlayerInput _playerInput;

        private CinemachineCamera _currentCamera;

        private void Awake()
        {
            _playerInput.Player.Enable();
        }

        private void OnEnable()
        {
            _playerInput.Player.Turnover.performed += ChangeCamera;
        }

        private void OnDisable()
        {
            _playerInput.Player.Turnover.performed -= ChangeCamera;
            _playerInput.Player.Turnover.canceled -= ChangeCamera;
        }

        private void ChangeCamera(InputAction.CallbackContext ctx)
        {
            ChangeCamera();
        }

        private void ChangeCamera()
        {
            _currentCamera = CameraViewChanger.Instance._currentCamera;
            
            int index = Array.IndexOf(_virtualCameras, _currentCamera);
            changeCamera?.Invoke(index == 0  ? _virtualCameras[1] : _virtualCameras[0]);
        }
    }
}