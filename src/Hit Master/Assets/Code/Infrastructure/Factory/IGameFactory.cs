using Code.Infrastructure.Pooling;
using Code.Infrastructure.Services;
using Code.Weapon;

namespace Code.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        PoolContainer CreateBulletPool(string assetPath);
        Gun CreateRandomGun(PoolContainer bulletPool);
        Gun CreateGun(int id, PoolContainer bulletPool);
    }
}