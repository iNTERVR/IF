using EcsRx.Zenject;
using EcsRx.Infrastructure.Extensions;
using UnityEngine;
using Inter.Installer;
using Inter.Modules.ToolModule;
using Inter.Prototype.Installer;

namespace Inter.Prototype
{
    public class PrototypeBootstrap : EcsRxApplicationBehaviour
    {
        protected override void BindSystems()
        {
            base.BindSystems();

            // inter system setup
            Container.BindApplicableSystems(
                "Inter.Systems",
                "Inter.ViewResolvers"
                );
        }

        protected override void LoadModules()
        {
            base.LoadModules();

            Container.LoadModule<ToolModuleSetupDependency>();
        }

        protected override void LoadPlugins()
        {
            base.LoadPlugins();
        }

        protected override void ApplicationStarted()
        {
            var settings = Container.Resolve<PrototypeInstaller.Settings>();
            var interSettings = Container.Resolve<InterInstaller.Settings>();
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
