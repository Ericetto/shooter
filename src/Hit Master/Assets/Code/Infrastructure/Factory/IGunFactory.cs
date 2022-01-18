using Pooling;
using Code.Weapon;

namespace Code.Infrastructure.Factory
{
    internal interface IGunFactory
    {
        IGun CreateRandomGun(IPoolContainer bulletPool);
        IGun CreateGun(int id, IPoolContainer bulletPool);
    }
}