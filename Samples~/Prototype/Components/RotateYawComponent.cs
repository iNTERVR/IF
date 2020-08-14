using EcsRx.Components;
using EcsRx.Entities;
using EcsRx.Extensions;
using UnityEngine;

namespace InterVR.Unity.SDK.InterFramework.Prototype.Components
{
    public class RotateYaw : IComponent
    {
        public float Speed { get; set; }
    }

    public class RotateYawComponent : MonoBehaviour, IConvertToEntity
    {
        public float Speed = 30.0f;

        public void Convert(IEntity entity, IComponent component = null)
        {
            var c = component == null ? new RotateYaw() : component as RotateYaw;

            c.Speed = Speed;

            entity.AddComponentSafe(c);

            Destroy(this);
        }
    }
}