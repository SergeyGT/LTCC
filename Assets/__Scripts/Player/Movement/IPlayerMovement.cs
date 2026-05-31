using UnityEngine;

namespace __Scripts.Player
{
    public interface IPlayerMovement
    {
        public Vector3 ReadMovement(Vector2 input, float gravityForce, float verticalVelocity, Transform cameraRotate);
        public Quaternion Rotation(Transform cameraRotate);
    }
}