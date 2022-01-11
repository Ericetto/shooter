using Code.Infrastructure.Services;

namespace Code.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        PoolContainer CreateBulletPool();
        PoolContainer CreateEnemyPool();
    }
}