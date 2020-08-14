using InterVR.Unity.SDK.InterFramework.Components;
using InterVR.Unity.SDK.InterFramework.Defines;
using EcsRx.Entities;
using EcsRx.Extensions;
using EcsRx.Groups;
using EcsRx.Plugins.ReactiveSystems.Systems;
using EcsRx.Unity.Extensions;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using EcsRx.Collections.Database;

namespace InterVR.Unity.SDK.InterFramework.Systems
{
    public class InterFollowEntitySystem : ISetupSystem, ITeardownSystem
    {
        public IGroup Group => new Group(typeof(InterFollowEntity));

        private Dictionary<IEntity, List<IDisposable>> subscriptionsPerEntity = new Dictionary<IEntity, List<IDisposable>>();
        private readonly IEntityDatabase entityDatabase;

        public InterFollowEntitySystem(IEntityDatabase entityDatabase)
        {
            this.entityDatabase = entityDatabase;
        }

        public void Setup(IEntity entity)
        {
            var subscriptions = new List<IDisposable>();
            subscriptionsPerEntity.Add(entity, subscriptions);

            var followEntity = entity.GetComponent<InterFollowEntity>();
            if (followEntity.UpdateMoment == UpdateMomentType.Update) Observable.EveryUpdate().Subscribe(x => follow(entity, followEntity)).AddTo(subscriptions);
            else if (followEntity.UpdateMoment == UpdateMomentType.FixedUpdate) Observable.EveryFixedUpdate().Subscribe(x => follow(entity, followEntity)).AddTo(subscriptions);
            else if (followEntity.UpdateMoment == UpdateMomentType.LateUpdate) Observable.EveryLateUpdate().Subscribe(x => follow(entity, followEntity)).AddTo(subscriptions);
        }

        public void Teardown(IEntity entity)
        {
            if (subscriptionsPerEntity.TryGetValue(entity, out List<IDisposable> subscriptions))
            {
                subscriptions.DisposeAll();
                subscriptions.Clear();
                subscriptionsPerEntity.Remove(entity);
            }
        }

        void follow(IEntity entity, InterFollowEntity followEntity)
        {
            if (entityDatabase.GetCollectionFor(followEntity.FollowSourceEntity) == null ||
                entityDatabase.GetCollectionFor(followEntity.FollowTargetEntity) == null)
            {
                entityDatabase.RemoveEntity(entity);
                return;
            }

            if (followEntity.FollowPosition) followPosition(followEntity);
            if (followEntity.FollowRotation) followRotation(followEntity);
        }

        void followPosition(InterFollowEntity followEntity)
        {
            var sourceView = followEntity.FollowSourceEntity.GetGameObject();
            var targetView = followEntity.FollowTargetEntity.GetGameObject();
            if (sourceView == null || targetView == null)
                return;

            var transformToSource = followEntity.FollowSourceEntity.GetGameObject().transform;
            var transformToTarget = followEntity.FollowTargetEntity.GetGameObject().transform;

            Vector3 targetTransformPosition = transformToTarget.TransformPoint(followEntity.OffsetPosition);
            Vector3 newPosition;

            if (followEntity.SmoothPosition)
            {
                float alpha = Mathf.Clamp01(Vector3.Distance(followEntity.CalculatedPosition, targetTransformPosition) / followEntity.MaxDistanceDeltaPerFrame);
                newPosition = Vector3.Lerp(followEntity.CalculatedPosition, targetTransformPosition, alpha);
            }
            else
            {
                newPosition = targetTransformPosition;
            }
            followEntity.CalculatedPosition = newPosition;
            
            transformToSource.position = followEntity.CalculatedPosition;
        }

        void followRotation(InterFollowEntity followEntity)
        {
            var sourceView = followEntity.FollowSourceEntity.GetGameObject();
            var targetView = followEntity.FollowTargetEntity.GetGameObject();
            if (sourceView == null || targetView == null)
                return;

            var transformToSource = followEntity.FollowSourceEntity.GetGameObject().transform;
            var transformToTarget = followEntity.FollowTargetEntity.GetGameObject().transform;

            Quaternion targetTransformRotation = transformToTarget.rotation * Quaternion.Euler(followEntity.OffsetRotation);
            Quaternion newRotation;

            if (followEntity.SmoothRotation)
            {
                float alpha = Mathf.Clamp01(Quaternion.Angle(Quaternion.Euler(followEntity.CalculatedRotation), targetTransformRotation) / followEntity.MaxDistanceDeltaPerFrame);
                newRotation = Quaternion.Lerp(Quaternion.Euler(followEntity.CalculatedRotation), targetTransformRotation, alpha);
            }
            else
            {
                newRotation = targetTransformRotation;
            }
            followEntity.CalculatedRotation = newRotation.eulerAngles;

            transformToSource.rotation = Quaternion.Euler(followEntity.CalculatedRotation);
        }
    }
}