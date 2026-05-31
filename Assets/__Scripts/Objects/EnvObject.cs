using __Scripts.System;
using UnityEngine;

namespace __Scripts.Objects
{
    public abstract class EnvObject : InteractableObject
    {
        [SerializeField] protected SerializableDictionary<string, AnimationClip> _animationClips;
        public override void Interact()
        {
            
        }
        
        public abstract void PlayAnimation();

        public override bool CanInteract()
        {
            return false;
        }

        public override void OnFocusEnter()
        {
            
        }

        public override void OnFocusExit()
        {
            
        }
    }
}