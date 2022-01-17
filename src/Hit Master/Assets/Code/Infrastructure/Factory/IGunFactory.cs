using Code.Infrastructure.Pooling;
using Code.Weapon;

namespace Code.Infrastructure.Factory
{
    public interface IGunFactory
    {
        IGun CreateRandomGun(IPoolContainer bulletPool);
        IGun CreateGun(int id, IPoolContainer bulletPool);
    }
}