using System;
using __Scripts.Player;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour, IAnimationHandler
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetAnimator(Animator animator)
    {
            
    }
    public void TriggerJump()
    {
       
    }

    public void TriggerSwitchMovement()
    {
        
    }

    public void SetSpeed(float speed)
    {
        _animator.SetFloat("Speed", speed);
        print(speed);
    }
}
