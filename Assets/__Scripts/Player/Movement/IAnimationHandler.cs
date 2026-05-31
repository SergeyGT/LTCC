using UnityEngine;

namespace __Scripts.Player
{
    public interface IAnimationHandler
    {
        public void Crouch(bool crouch);
        public void TriggerJump();
        public void TriggerSwitchMovement();
        public void SetSpeed(float speed);
        public void SetAnimator(Animator animator);
        
    }
}