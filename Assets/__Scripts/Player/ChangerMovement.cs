using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace __Scripts.Player
{
    public class ChangerMovement : MonoBehaviour
    {
        public static event UnityAction<IPlayerMovement> changeMovement;
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
                changeMovement?.Invoke(new SidePersonMovement());
            }
            else if (side == "first")
            {
                changeMovement?.Invoke(new FirstPersonMovement());
            }
            
        }
    }
}