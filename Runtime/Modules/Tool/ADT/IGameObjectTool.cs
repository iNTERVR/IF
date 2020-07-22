﻿using Inter.Defines;
using EcsRx.Unity.Dependencies;
using UnityEngine;

namespace Inter.Modules.ToolModule
{
    public interface IGameObjectTool
    {
        GameObject InstantiateWithInit(GameObject prefab, Transform parent = null);
        GameObject InstantiateWithInit(IUnityInstantiator instantiator, GameObject prefab, Transform parent = null);
        void SetParentWithInit(GameObject go, Transform parent);
    }
}
