using EcsRx.Entities;
using EcsRx.Extensions;
using EcsRx.Groups;
using EcsRx.Plugins.ReactiveSystems.Systems;
using EcsRx.Plugins.Views.Components;
using System;
using UniRx;
using System.Collections.Generic;
using InterVR.IF.Prototype.Components;
using EcsRx.Unity.Extensions;
using UnityEngine;

namespace InterVR.IF.Prototype.Systems
{
    public class RotateYawSystem : ISetupSystem, ITeardownSystem
    {
        public IGroup Group => new Group(typeof(RotateYaw), typeof(ViewComponent));

        private Dictionary<IEntity, List<IDisposable>> subscriptionsPerEntity = new Dictionary<IEntity, List<IDisposable>>();

        public void Setup(IEntity entity)
        {
            List<IDisposable> subscriptions = new List<IDisposable>();
            subscriptionsPerEntity.Add(entity, subscriptions);

            var view = entity.GetGameObject();
            var rotateYaw = entity.GetComponent<RotateYaw>();

            Observable.EveryUpdate()
                .Subscribe(x =>
                {
                    view.transform.Rotate(new Vector3(0, rotateYaw.Speed * Time.deltaTime, 0));

                }).AddTo(subscriptions);
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
    }
}