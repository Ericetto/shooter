using Code.Infrastructure.Pooling;
using Code.Infrastructure.Services;
using Code.Weapon;

namespace Code.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        PoolContainer CreatePool(string assetPath);
        Gun CreateRandomGun(IPoolContainer bulletPool);
        Gun CreateGun(int id, IPoolContainer bulletPool);
    }
}