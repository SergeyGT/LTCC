using UnityEngine;

namespace __Scripts.Player
{
    public class FirstPersonMovement : IPlayerMovement
    {
        public Quaternion Rotation(Transform cameraRotate)
        {
            return Quaternion.Euler(new Vector3(0, cameraRotate.eulerAngles.y, 0));
        }

        public Vector3 ReadMovement(Vector2 input, float gravityForce, float verticalVelocity, Transform cameraRotate)
        {
            Debug.Log("FirstPersonMovement.ReadMovement");
            
            Vector2 directionInput = input;
        
            Vector3 moveDirection = new Vector3(directionInput.x, verticalVelocity, directionInput.y);
        
            moveDirection = Quaternion.Euler(0, cameraRotate.eulerAngles.y, 0) * moveDirection;
            
            return moveDirection;
        }
    }
}