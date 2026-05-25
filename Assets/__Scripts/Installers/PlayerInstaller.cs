using __Scripts.Player;
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
        Container.Bind<PlayerMovement>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IPlayerMovement>().
            To<FirstPersonMovement>().
            AsSingle();
        
        Container.Bind<IAnimationHandler>().
            WithId(BindID.Player).
            To<PlayerAnimator>().
            FromComponentInHierarchy().
            AsSingle();
    }
}
