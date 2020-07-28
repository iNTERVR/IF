using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Inter.Prototype.Installer
{
    public class PrototypeMonoInstaller : MonoInstaller<PrototypeMonoInstaller>
    {
        public List<ScriptableObjectInstaller> settings;

        public override void InstallBindings()
        {
            var settingsInstaller = settings.Cast<IInstaller>();
            foreach (var installer in settingsInstaller)
            {
                Container.Inject(installer);
                installer.InstallBindings();
            }
        }
    }
}