using System;
using System.Collections.Generic;
using System.ComponentModel;
using __Scripts.Cameras;
using __Scripts.Player;
using __Scripts.Player.Interact;
using NUnit.Framework;
using R3;
using UnityEngine;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine.Events;
using Camera = __Scripts.Cameras.CameraData;

public class CameraViewChanger : MonoBehaviour
{
    public static CameraViewChanger Instance;
    public static event UnityAction<string> changeCameraView;
    public static event UnityAction<Transform> changeCameraRotate;
    
    [SerializeField] private List<CinemachineCamera> _virtualCameras;
    
    public CinemachineCamera _currentCamera => FindCameraWithHighPriority();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            TriggerCameraViewOnchangeCamera(_currentCamera);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        PlayerInteract.changeCamera += TriggerCameraViewOnchangeCamera;
    }

    private void OnDisable()
    {
        PlayerInteract.changeCamera -= TriggerCameraViewOnchangeCamera;
    }

    private void TriggerCameraViewOnchangeCamera(CinemachineCamera _camera)
    {
        CinemachineCamera currentCamera = FindCameraWithHighPriority();
        (currentCamera.Priority, _camera.Priority) = (_camera.Priority, currentCamera.Priority);
        changeCameraView?.Invoke(_camera.GetComponent<CameraData>().GetSideCamera());
        changeCameraRotate?.Invoke(_camera.GetComponent<Transform>());
    }

    private CinemachineCamera FindCameraWithHighPriority()
    {
        CinemachineCamera maxPriorityCamera = null;
        int maxPriority = -1;

        foreach (var cam in _virtualCameras)
        {
            if (cam != null && cam.Priority > maxPriority)
            {
                maxPriority = cam.Priority;
                maxPriorityCamera = cam;
            }
        }

        if(maxPriority == -1) throw new WarningException("Камера с наивысшим приоритетом не найдена");
        
        return maxPriorityCamera;
    }
}
