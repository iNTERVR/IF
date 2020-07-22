using Inter.Defines;
using EcsRx.Components;
using EcsRx.Entities;
using UnityEngine;
using EcsRx.Extensions;

namespace Inter.Components
{
    public class InterFollowTransform : IComponent
    {
        public UpdateMomentType UpdateMoment { get; set; }
        public bool FollowPosition { get; set; }
        public bool FollowRotation { get; set; }
        public bool SmoothPosition { get; set; }
        public bool SmoothRotation { get; set; }
        public Vector3 OffsetPosition { get; set; }
        public Vector3 OffsetRotation { get; set; }
        public Transform FollowTargetTransform{ get; set; }
        public Transform FollowSourceTransform { get; set; }
        public float MaxDistanceDeltaPerFrame { get; set; }

        public Vector3 CalculatedPosition;
        public Vector3 CalculatedRotation;
    }

    public class InterFollowTransformComponent : MonoBehaviour, IConvertToEntity
    {
        public UpdateMomentType UpdateMoment = UpdateMomentType.Update;
        public Transform FollowSourceTransform;
        public Transform FollowTargetTransform;
        public bool FollowPosition = true;
        public bool FollowRotation = true;
        public bool SmoothPosition;
        public bool SmoothRotation;
        public Vector3 OffsetPosition;
        public Vector3 OffsetRotation;
        public float MaxDistanceDeltaPerFrame = 0.003f;

        public void Convert(IEntity entity, IComponent component = null)
        {
            var c = component == null ? new InterFollowTransform() : component as InterFollowTransform;

            c.UpdateMoment = UpdateMoment;
            c.FollowPosition = FollowPosition;
            c.FollowRotation = FollowRotation;
            c.SmoothPosition = SmoothPosition;
            c.SmoothRotation = SmoothRotation;
            c.OffsetPosition = OffsetPosition;
            c.OffsetRotation = OffsetRotation;
            c.MaxDistanceDeltaPerFrame = MaxDistanceDeltaPerFrame;

            c.FollowSourceTransform = FollowSourceTransform;
            c.FollowTargetTransform = FollowTargetTransform;

            entity.AddComponentSafe(c);

            Destroy(this);
        }
    }
}