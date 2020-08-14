using EcsRx.Zenject;
using EcsRx.Infrastructure.Extensions;
using UnityEngine;
using InterVR.Unity.SDK.InterFramework.Installer;
using InterVR.Unity.SDK.InterFramework.Prototype.Installer;

namespace InterVR.Unity.SDK.InterFramework.Prototype
{
    public class PrototypeBootstrap : InterFrameworkApplicationBehaviour
    {
        protected override void ApplicationStarted()
        {
            var settings = Container.Resolve<PrototypeInstaller.Settings>();
            var interSettings = Container.Resolve<InterFrameworkInstaller.Settings>();
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
