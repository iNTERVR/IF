using EcsRx.Zenject;
using EcsRx.Infrastructure.Extensions;
using UnityEngine;
using InterVR.IF.Installer;
using InterVR.IF.Prototype.Installer;

namespace InterVR.IF.Prototype
{
    public class PrototypeBootstrap : IF_ApplicationBehaviour
    {
        protected override void ApplicationStarted()
        {
            var settings = Container.Resolve<PrototypeInstaller.Settings>();
            var interSettings = Container.Resolve<IF_Installer.Settings>();
            Debug.Log($"settings.Name is {settings.Name} in {interSettings.Name}");
        }

        private void OnDestroy()
        {
            StopAndUnbindAllSystems();
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause == false)
            {
            }
        }

        private void OnApplicationFocus(bool focus)
        {

        }
    }
}
