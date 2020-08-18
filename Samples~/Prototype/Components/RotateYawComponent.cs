using EcsRx.Components;
using EcsRx.Entities;
using EcsRx.Extensions;
using EcsRx.UnityEditor.MonoBehaviours;

namespace InterVR.IF.Prototype.Components
{
    public class RotateYaw : IComponent
    {
        public float Speed { get; set; }
    }

    public class RotateYawComponent : RegisterAsEntity, IConvertToEntity
    {
        public float Speed = 30.0f;

        public override void Convert(IEntity entity, IComponent component = null)
        {
            var c = component == null ? new RotateYaw() : component as RotateYaw;

            c.Speed = Speed;

            entity.AddComponentSafe(c);

            Destroy(this);
        }
    }
}