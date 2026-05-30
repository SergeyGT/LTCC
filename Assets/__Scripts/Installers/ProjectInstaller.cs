using __Scripts.Managers;
using Zenject;

namespace __Scripts.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindManagers();
        }

        private void BindManagers()
        {
            Container.Bind<GameManager>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        }

        private void BindSignaBus()
        {
            SignalBusInstaller.Install(Container);
        }
    }
}