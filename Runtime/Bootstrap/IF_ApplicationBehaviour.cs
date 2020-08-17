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
        protected override void ApplicationStarted()
        {
            this.EventSystem.Publish(new IF_ApplicationStartedEvent() { });
        }
    }
}