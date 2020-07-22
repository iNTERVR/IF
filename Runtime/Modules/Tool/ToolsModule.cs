using EcsRx.Infrastructure.Dependencies;
using EcsRx.Infrastructure.Extensions;

namespace Inter.Modules.ToolModule
{
    public class ToolModuleSetupDependency : IDependencyModule
    {
        public void Setup(IDependencyContainer container)
        {
            container.Bind<IGameObjectTool, GameObjectTool>();
        }
    }
}
