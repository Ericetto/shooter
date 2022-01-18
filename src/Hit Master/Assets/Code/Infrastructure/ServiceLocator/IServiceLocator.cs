namespace Code.Infrastructure.ServiceLocator
{
    public interface IServiceLocator
    {
        TService Single<TService>() where TService : IService;
        void RegisterSingle<TService>(TService implementation) where TService : IService;
    }
}