using System;
using R3;
using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class TriggerCameraView : MonoBehaviour
{
    public static event UnityAction<CinemachineCamera> changeCamera;
    
    [Header("Камера на которую произвести переключение")]
    [SerializeField] private CinemachineCamera _virtualCamera;
    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        changeCamera?.Invoke(_virtualCamera);
        _collider.enabled = false;
    }

}
