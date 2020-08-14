using EcsRx.Infrastructure.Dependencies;
using EcsRx.Infrastructure.Extensions;

namespace InterVR.IF.Modules
{
    public class IF_ToolModules : IDependencyModule
    {
        public void Setup(IDependencyContainer container)
        {
            container.Bind<IF_IGameObjectTool, IF_GameObjectTool>();
        }
    }
}
