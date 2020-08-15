using System;
using UnityEngine;
using Zenject;

namespace InterVR.IF.Installer
{
    [CreateAssetMenu(fileName = "IF_Settings", menuName = "InterVR/IF/Settings")]
    public class IF_Installer : ScriptableObjectInstaller<IF_Installer>
    {
#pragma warning disable 0649
        [SerializeField]
        Settings settings;
#pragma warning restore 0649

        public override void InstallBindings()
        {
            Container.BindInstance(settings).IfNotBound();

            checkInstaller("intervr.if.vr");
            checkInstaller("intervr.if.vr.plugin.steam");
            checkInstaller("intervr.if.glove");
            checkInstaller("intervr.if.glove.plugin.manus");
        }

        void checkInstaller(string path)
        {
            var prefab = Resources.Load(path) as GameObject;
            if (prefab == null)
                return;

            Debug.Log($"found {path} package to install");

            var instance = GameObject.Instantiate(prefab.gameObject);
            instance.transform.SetParent(ProjectContext.Instance.transform, false);
            var installer = instance.GetComponent<MonoInstaller>();
            Container.Inject(installer);
            installer.InstallBindings();
        }

        [Serializable]
        public class Settings
        {
            public string Name = "IF Installer";
        }
    }
}