﻿using System;
using UnityEngine;
using Zenject;

namespace Inter.Prototype.Installer
{
    [CreateAssetMenu(fileName = "PrototypeSettings", menuName = "Inter/Mods/Prototype/Settings")]
    public class PrototypeInstaller : ScriptableObjectInstaller<PrototypeInstaller>
    {
#pragma warning disable 0649
        [SerializeField]
        Settings settings;
#pragma warning restore 0649

        public override void InstallBindings()
        {
            Container.BindInstance(settings).IfNotBound();
        }

        [Serializable]
        public class Settings
        {
            public string Name = "Prototype Installer";
        }
    }
}