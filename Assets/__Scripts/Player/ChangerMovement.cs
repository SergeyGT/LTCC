using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace __Scripts.Player
{
    public class ChangerMovement : MonoBehaviour
    {
        public static event UnityAction<IPlayerMovement> changeMovement;

        private IPlayerMovement sidePerson;
        private IPlayerMovement firstPerson;

        private void Awake()
        {
            sidePerson = gameObject.AddComponent<SidePersonMovement>();
            firstPerson = gameObject.AddComponent<FirstPersonMovement>();
        }

        private void OnEnable()
        {
            CameraViewChanger.changeCameraView += ChangeMovement;
        }

        private void OnDisable()
        {
            CameraViewChanger.changeCameraView -= ChangeMovement;
        }

        private void ChangeMovement(string side)
        {
            if (side == "side")
            {
                changeMovement?.Invoke(sidePerson);
            }
            else if (side == "first")
            {
                changeMovement?.Invoke(firstPerson);
            }
            
        }
    }
}