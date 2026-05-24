using __Scripts.Player;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour, IAnimationHandler
{
    private Animator _animator;

    public void SetAnimator(Animator animator)
    {
        _animator = animator;
    }
    public void TriggerJump()
    {
        print("Jump");
    }

    public void TriggerSwitchMovement()
    {
        print("Switch Movement");
    }

    public void SetSpeed(float speed)
    {
        print("Speed: " + speed);
    }
}
