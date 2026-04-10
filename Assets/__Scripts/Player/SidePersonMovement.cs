using UnityEngine;

namespace __Scripts.Player
{
    public class SidePersonMovement : MonoBehaviour, IPlayerMovement
    {
        private Vector3 _moveDirection;
        
        public Quaternion Rotation(Transform cameraRotate)
        {
            Vector3 horizontalDirection = new Vector3(_moveDirection.x, 0f, _moveDirection.z);
            
            if (horizontalDirection.sqrMagnitude < 0.001f)
                return transform.rotation; 
                
            return Quaternion.LookRotation(horizontalDirection);
        }

        public Vector3 ReadMovement(Vector2 input, float gravityForce, float verticalVelocity, Transform cameraRotate)
        {
            Vector2 directionInput = input;
        
            _moveDirection = new Vector3(directionInput.x, verticalVelocity, directionInput.y);
        
           _moveDirection = Quaternion.Euler(0, cameraRotate.eulerAngles.y, 0) * _moveDirection;
            
            return _moveDirection;
        }
    }
}