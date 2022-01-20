using Code.Infrastructure.Services;

namespace Code.Infrastructure.Factory
{
    public interface IGameFactory : IService, IGunFactory, IPoolFactory, IPathFactory
    {

    }
}