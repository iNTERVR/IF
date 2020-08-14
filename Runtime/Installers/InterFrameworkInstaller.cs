using System;
using UnityEngine;
using Zenject;

namespace InterVR.Unity.SDK.InterFramework.Installer
{
    [CreateAssetMenu(fileName = "InterFrameworkSettings", menuName = "InterVR/InterFramework/Settings")]
    public class InterFrameworkInstaller : ScriptableObjectInstaller<InterFrameworkInstaller>
    {
#pragma warning disable 0649
        [SerializeField]
        Settings settings;
#pragma warning restore 0649

        public override void InstallBindings()
        {
            Container.BindInstance(settings).IfNotBound();

            checkSDKInstaller("intervr.unity.sdk.steamvr");
            checkSDKInstaller("intervr.unity.sdk.manusvr");
        }

        void checkSDKInstaller(string path)
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
            public string Name = "InterFramework Installer";
        }
    }
}