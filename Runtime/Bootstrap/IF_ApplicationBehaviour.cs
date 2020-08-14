using EcsRx.Infrastructure.Extensions;
using EcsRx.Zenject;
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
                "InterVR.IF.VR.Systems",
                "InterVR.IF.VR.ViewResolvers");
            Container.BindApplicableSystems(
                "InterVR.IF.VR.GLOVE.Systems",
                "InterVR.IF.VR.GLOVE.ViewResolvers");
        }

        protected override void LoadModules()
        {
            base.LoadModules();

            Container.LoadModule<IF_ToolModule>();
        }

        protected override void LoadPlugins()
        {
            base.LoadPlugins();
        }
    }
}