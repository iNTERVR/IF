using EcsRx.Infrastructure.Extensions;
using EcsRx.Zenject;
using InterVR.IF.Events;
using InterVR.IF.Modules;
using System.Collections;
using UnityEngine;

namespace InterVR.IF
{
    [DefaultExecutionOrder(-20000)]
    public abstract class IF_ApplicationBehaviour : EcsRxApplicationBehaviour
    {
        protected override void BindSystems()
        {
            base.BindSystems();

            Container.BindApplicableSystems(
                "InterVR.IF.Systems",
                "InterVR.IF.ViewResolvers");
        }

        protected override void LoadModules()
        {
            base.LoadModules();

            Container.LoadModule<IF_ToolModules>();
            Container.LoadModule<IF_PluginModules>();
        }

        protected override IEnumerator ApplicationStartedAsync()
        {
            yield return Container.InitializeModules();
            yield return base.ApplicationStartedAsync();
        }

        protected override void ApplicationStarted()
        {
            EventSystem.Publish(new IF_ApplicationStartedEvent() { });
        }

        protected virtual void OnDestroy()
        {
            Container.UnloadModules();
            StopAndUnbindAllSystems();
        }
    }
}