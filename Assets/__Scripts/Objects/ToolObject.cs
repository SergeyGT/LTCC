namespace __Scripts.Objects
{
    public abstract class ToolObject : InteractableObject
    {
        public override void Interact()
        {
            
        }
        
        
        

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