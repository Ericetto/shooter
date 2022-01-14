using Code.Infrastructure.Pooling;
using Code.Infrastructure.Services;
using Code.Weapon;

namespace Code.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        IPoolContainer CreatePool(string assetPath);
        IGun CreateRandomGun(IPoolContainer bulletPool);
        IGun CreateGun(int id, IPoolContainer bulletPool);
    }
}