using __Scripts.Player;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    override public void InstallBindings()
    {
        Container.Bind<IPlayerMovement>().
            To<FirstPersonMovement>().
            AsSingle();
    }
}
