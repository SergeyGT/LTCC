using System;
using __Scripts.Player;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour, IAnimationHandler
{
    private Animator _animator;
    private float _speedCharacter;
    [Header("Параметр влияющий на скорость в анимации")]
    [SerializeField] private float _speedAnimFactor;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetAnimator(Animator animator)
    {
            
    }
    public void TriggerJump()
    {
       //_animator.SetTrigger("Jump");
    }

    public void TriggerSwitchMovement()
    {
        
    }

    public void SetSpeed(float speed)
    {
        _animator.SetFloat("Speed", speed);
        _speedCharacter = speed;
    }

    private void Update()
    {
        _animator.speed = Mathf.Max(1, _speedCharacter /  _speedAnimFactor);
    }
}
