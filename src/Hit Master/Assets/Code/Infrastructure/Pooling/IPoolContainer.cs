using Code.Infrastructure.Services;

namespace Code.Infrastructure.Pooling
{
    public interface IPoolContainer : IService
    {
        PoolObject Get();
        void Recycle(PoolObject obj);
    }
}