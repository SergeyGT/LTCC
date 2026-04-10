using __Scripts.Enums;
using __Scripts.Player.Interact;
using UnityEngine;

namespace __Scripts.Objects
{
    public abstract class InteractableObject : MonoBehaviour, Interactable
    {
        [SerializeField] private TypeInteractable _typeInteractable;
        public abstract void Interact();
        public abstract bool CanInteract();
        public abstract void OnFocusEnter();
        public abstract void OnFocusExit();
    }
    
}