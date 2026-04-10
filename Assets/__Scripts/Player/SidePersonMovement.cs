using Unity.VisualScripting;
using UnityEngine;

namespace __Scripts.Player
{
    public class SidePersonMovement : MonoBehaviour, IPlayerMovement
    {
        private Vector3 _moveDirection;
        private Vector3 _lastHorizontalDirection = Vector3.forward;
        
        public Quaternion Rotation(Transform cameraRotate)
        {
            Debug.DrawRay(_moveDirection, Vector3.up, Color.red);
            Debug.Log("RotateSide");
            Vector3 horizontalDirection = new Vector3(_moveDirection.x, 0f, _moveDirection.z);

            if (horizontalDirection.sqrMagnitude > 0.01f)
            {
                _lastHorizontalDirection = horizontalDirection.normalized;
            }

            return Quaternion.LookRotation(_lastHorizontalDirection, Vector3.up);
        }

        public Vector3 ReadMovement(Vector2 input, float gravityForce, float verticalVelocity, Transform cameraRotate)
        {
            Vector2 directionInput = input;
        
            _moveDirection = new Vector3(directionInput.x, verticalVelocity, directionInput.y);
        
            _moveDirection = Quaternion.Euler(0f, cameraRotate.eulerAngles.y, 0f) * _moveDirection;
            
            return _moveDirection;
        }
    }
}