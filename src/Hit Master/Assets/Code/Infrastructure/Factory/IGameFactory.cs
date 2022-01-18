using Code.Infrastructure.ServiceLocator;

namespace Code.Infrastructure.Factory
{
    internal interface IGameFactory : IService, IGunFactory, IPoolFactory, IWayFactory
    {

    }
}