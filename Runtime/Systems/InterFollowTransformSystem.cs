using Inter.Components;
using Inter.Defines;
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

namespace Inter.Systems
{
    public class InterFollowTransformSystem : ISetupSystem, ITeardownSystem
    {
        public IGroup Group => new Group(typeof(InterFollowTransform));

        private Dictionary<IEntity, List<IDisposable>> subscriptionsPerEntity = new Dictionary<IEntity, List<IDisposable>>();
        private readonly IEntityDatabase entityDatabase;

        public InterFollowTransformSystem(IEntityDatabase entityDatabase)
        {
            this.entityDatabase = entityDatabase;
        }

        public void Setup(IEntity entity)
        {
            var subscriptions = new List<IDisposable>();
            subscriptionsPerEntity.Add(entity, subscriptions);

            var followTransform = entity.GetComponent<InterFollowTransform>();
            if (followTransform.UpdateMoment == UpdateMomentType.Update) Observable.EveryUpdate().Subscribe(x => follow(entity, followTransform)).AddTo(subscriptions);
            else if (followTransform.UpdateMoment == UpdateMomentType.FixedUpdate) Observable.EveryFixedUpdate().Subscribe(x => follow(entity, followTransform)).AddTo(subscriptions);
            else if (followTransform.UpdateMoment == UpdateMomentType.LateUpdate) Observable.EveryLateUpdate().Subscribe(x => follow(entity, followTransform)).AddTo(subscriptions);
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

        void follow(IEntity entity, InterFollowTransform followTransform)
        {
            if (followTransform.FollowSourceTransform == null ||
                followTransform.FollowTargetTransform == null)
            {
                var view = entity.GetGameObject();
                if (view != null)
                {
                    GameObject.Destroy(view);
                }
                entityDatabase.RemoveEntity(entity);
                return;
            }

            if (followTransform.FollowPosition) followPosition(followTransform);
            if (followTransform.FollowRotation) followRotation(followTransform);
        }

        void followPosition(InterFollowTransform followTransform)
        {
            var transformToSource = followTransform.FollowSourceTransform;
            var transformToTarget = followTransform.FollowTargetTransform;

            Vector3 targetTransformPosition = transformToTarget.TransformPoint(followTransform.OffsetPosition);
            Vector3 newPosition;

            if (followTransform.SmoothPosition)
            {
                float alpha = Mathf.Clamp01(Vector3.Distance(followTransform.CalculatedPosition, targetTransformPosition) / followTransform.MaxDistanceDeltaPerFrame);
                newPosition = Vector3.Lerp(followTransform.CalculatedPosition, targetTransformPosition, alpha);
            }
            else
            {
                newPosition = targetTransformPosition;
            }
            followTransform.CalculatedPosition = newPosition;
            
            transformToSource.position = followTransform.CalculatedPosition;
        }

        void followRotation(InterFollowTransform followTransform)
        {
            var transformToSource = followTransform.FollowSourceTransform;
            var transformToTarget = followTransform.FollowTargetTransform;

            Quaternion targetTransformRotation = transformToTarget.rotation * Quaternion.Euler(followTransform.OffsetRotation);
            Quaternion newRotation;

            if (followTransform.SmoothRotation)
            {
                float alpha = Mathf.Clamp01(Quaternion.Angle(Quaternion.Euler(followTransform.CalculatedRotation), targetTransformRotation) / followTransform.MaxDistanceDeltaPerFrame);
                newRotation = Quaternion.Lerp(Quaternion.Euler(followTransform.CalculatedRotation), targetTransformRotation, alpha);
            }
            else
            {
                newRotation = targetTransformRotation;
            }
            followTransform.CalculatedRotation = newRotation.eulerAngles;

            transformToSource.rotation = Quaternion.Euler(followTransform.CalculatedRotation);
        }
    }
}