using EcsRx.Components;
using EcsRx.Entities;
using EcsRx.Extensions;
using EcsRx.UnityEditor.MonoBehaviours;
using Prototype.Defines;

namespace Prototype.Components
{
    public class ContentInfo : IComponent
    {
        public ContentType Type { get; set; }
    }

    public class ContentInfoComponent : RegisterAsEntity, IConvertToEntity
    {
        public ContentType Type;

        public override void Convert(IEntity entity, IComponent component = null)
        {
            var c = component == null ? new ContentInfo() : component as ContentInfo;

            c.Type = Type;

            entity.AddComponentSafe(c);

            Destroy(this);
        }
    }
}