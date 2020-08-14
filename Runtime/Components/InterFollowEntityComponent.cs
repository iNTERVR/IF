using InterVR.Unity.SDK.InterFramework.Defines;
using EcsRx.Components;
using EcsRx.Entities;
using UnityEngine;

namespace InterVR.Unity.SDK.InterFramework.Components
{
    public class InterFollowEntity : IComponent
    {
        public UpdateMomentType UpdateMoment { get; set; }
        public bool FollowPosition { get; set; }
        public bool FollowRotation { get; set; }
        public bool SmoothPosition { get; set; }
        public bool SmoothRotation { get; set; }
        public Vector3 OffsetPosition { get; set; }
        public Vector3 OffsetRotation { get; set; }
        public IEntity FollowTargetEntity { get; set; }
        public IEntity FollowSourceEntity { get; set; }
        public float MaxDistanceDeltaPerFrame { get; set; }

        public Vector3 CalculatedPosition;
        public Vector3 CalculatedRotation;
    }
}