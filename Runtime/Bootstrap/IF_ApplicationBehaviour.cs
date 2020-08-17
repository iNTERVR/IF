using EcsRx.Infrastructure.Extensions;
using EcsRx.Zenject;
using InterVR.IF.Events;
using InterVR.IF.Modules;
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
            Container.BindApplicableSystems(
                "InterVR.IF.VR.Systems",
                "InterVR.IF.VR.ViewResolvers");
            Container.BindApplicableSystems(
                "InterVR.IF.VR.Glove.Systems",
                "InterVR.IF.VR.Glove.ViewResolvers");
            Container.BindApplicableSystems(
                "InterVR.IF.VR.Plugin.Steam.Systems",
                "InterVR.IF.VR.Plugin.Steam.ViewResolvers");
            Container.BindApplicableSystems(
                "InterVR.IF.VR.Glove.Plugin.SteamVRManus.Systems",
                "InterVR.IF.VR.Glove.Plugin.SteamVRManus.ViewResolvers");
        }

        protected override void ApplicationStarted()
        {
            this.EventSystem.Publish(new IF_ApplicationStartedEvent() { });
        }
    }
}