using EcsRx.Infrastructure.Dependencies;
using EcsRx.Systems;
using System;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Inter.Installer
{
    [CreateAssetMenu(fileName = "InterSettings", menuName = "Inter/Settings")]
    public class InterInstaller : ScriptableObjectInstaller<InterInstaller>
    {
#pragma warning disable 0649
        [SerializeField]
        Settings settings;
#pragma warning restore 0649

        public override void InstallBindings()
        {
            Container.BindInstance(settings).IfNotBound();

            checkEngineInstaller("if.InterVR");
        }

        void checkEngineInstaller(string path)
        {
            var prefab = Resources.Load(path) as GameObject;
            if (prefab == null)
                return;

            Debug.Log($"found {path} library to install");

            var instance = GameObject.Instantiate(prefab.gameObject);
            instance.transform.SetParent(ProjectContext.Instance.transform, false);
            var installer = instance.GetComponent<MonoInstaller>();
            Container.Inject(installer);
            installer.InstallBindings();
        }

        [Serializable]
        public class Settings
        {
            public string Name = "Inter Installer";
        }
    }
}