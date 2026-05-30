using __Scripts.Player;
using __Scripts.Player.Interact;
using UnityEngine;
using UnityEngine.Android;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    // TODO - переименовать класс в управление
    public enum BindID
    {
        Player,
        Enemy
    }
    override public void InstallBindings()
    {
        BindControls();
        
        Container.Bind<IPlayerMovement>().
            To<FirstPersonMovement>().
            AsSingle();
        
        Container.Bind<IAnimationHandler>().
            WithId(BindID.Player).
            To<PlayerAnimator>().
            FromComponentInHierarchy().
            AsSingle();
    }

    private void BindControls()
    {
        PlayerInput playerInput = new PlayerInput();
        Container.Bind<PlayerMovement>().
            FromComponentInHierarchy().
            AsSingle();
        
        Container.Bind<PlayerInput>().
            FromInstance(playerInput).
            AsSingle();
        
        Container.Bind<PlayerInteract>().
            FromComponentInHierarchy().
            AsSingle();
    }
}
