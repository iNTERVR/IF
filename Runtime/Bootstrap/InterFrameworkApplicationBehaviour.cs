using EcsRx.Infrastructure.Extensions;
using InterVR.Unity.SDK.InterFramework.Modules.ToolModule;
using UnityEngine;

namespace EcsRx.Zenject
{
    [DefaultExecutionOrder(-20000)]
    public abstract class InterFrameworkApplicationBehaviour : EcsRxApplicationBehaviour
    {
        protected override void BindSystems()
        {
            base.BindSystems();

            Container.BindApplicableSystems(
                "InterVR.Unity.SDK.InterFramework.Systems",
                "InterVR.Unity.SDK.InterFramework.ViewResolvers");
            Container.BindApplicableSystems(
                "InterVR.Unity.SDK.SteamVR.Systems",
                "InterVR.Unity.SDK.SteamVR.ViewResolvers");
            Container.BindApplicableSystems(
                "InterVR.Unity.SDK.ManusVR.Systems",
                "InterVR.Unity.SDK.ManusVR.ViewResolvers");
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
    }
}