namespace __Scripts.Player.Interact
{
    public interface Interactable
    {
        public void Interact();
        public bool CanInteract();
        public void OnFocusEnter();
        public void OnFocusExit();
    }
}