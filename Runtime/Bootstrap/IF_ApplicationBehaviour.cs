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
                "InterVR.IF.Glove.Systems",
                "InterVR.IF.Glove.ViewResolvers");
        }
    }
}