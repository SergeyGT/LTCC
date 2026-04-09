using System;
using System.Collections.Generic;
using System.ComponentModel;
using NUnit.Framework;
using UnityEngine;
using Unity.Cinemachine;
using Unity.VisualScripting;

public class CameraViewChanger : MonoBehaviour
{
    [SerializeField] private List<CinemachineCamera> _virtualCameras;
    private CinemachineCamera _currentCamera => FindCameraWithHighPriority();

    private void OnEnable()
    {
        TriggerCameraView.changeCamera += TriggerCameraViewOnchangeCamera;
    }

    private void TriggerCameraViewOnchangeCamera(CinemachineCamera _camera)
    {
        CinemachineCamera currentCamera = FindCameraWithHighPriority();
        (currentCamera.Priority, _camera.Priority) = (_camera.Priority, currentCamera.Priority);
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
