using Inter.Components;
using Inter.Defines;
using EcsRx.Blueprints;
using EcsRx.Entities;
using EcsRx.Extensions;
using UnityEngine;

namespace Inter.Blueprints
{
    public class InterFollowEntityBlueprint : IBlueprint
    {
        public InterFollowEntityBlueprint(UpdateMomentType updateMoment,
            IEntity targetEntity,
            IEntity sourceEntity,
            bool followPosition,
            bool followRotation,
            Vector3 offsetPosition,
            Vector3 offsetRotation,
            float maxDistanceDeltaPerFrame = 0.003f,
            bool smoothPosition = false,
            bool smoothRotation = false)
        {
            UpdateMoment = updateMoment;
            TargetEntity = targetEntity;
            SourceEntity = sourceEntity;
            FollowPosition = followPosition;
            FollowRotation = followRotation;
            OffsetPosition = offsetPosition;
            OffsetRotation = offsetRotation;
            MaxDistanceDeltaPerFrame = maxDistanceDeltaPerFrame;
            SmoothPosition = smoothPosition;
            SmoothRotation = smoothRotation;
        }

        public UpdateMomentType UpdateMoment { get; }
        public IEntity TargetEntity { get; }
        public IEntity SourceEntity { get; }
        public bool FollowPosition { get; }
        public bool FollowRotation { get; }
        public Vector3 OffsetPosition { get; }
        public Vector3 OffsetRotation { get; }
        public float MaxDistanceDeltaPerFrame { get; }
        public bool SmoothPosition { get; }
        public bool SmoothRotation { get; }

        public void Apply(IEntity entity)
        {
            entity.AddComponent(new InterFollowEntity()
            {
                UpdateMoment = UpdateMoment,
                MaxDistanceDeltaPerFrame = MaxDistanceDeltaPerFrame,
                FollowTargetEntity = TargetEntity,
                FollowSourceEntity = SourceEntity,
                FollowPosition = FollowPosition,
                FollowRotation = FollowRotation,
                OffsetPosition = OffsetPosition,
                OffsetRotation = OffsetRotation,
                SmoothPosition = SmoothPosition,
                SmoothRotation = SmoothRotation,
                CalculatedPosition = Vector3.zero,
                CalculatedRotation = Vector3.zero
            });
        }
    }
}