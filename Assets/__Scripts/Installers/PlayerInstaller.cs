using __Scripts.Player;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    override public void InstallBindings()
    {
        Container.Bind<PlayerMovement>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IPlayerMovement>().
            To<FirstPersonMovement>().
            AsSingle();
        
        Container.Bind<IAnimationHandler>().To<PlayerAnimator>().FromNewComponentOnNewGameObject().AsSingle();
    }
}
