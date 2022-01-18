using Code.Infrastructure.ServiceLocator;

namespace Code.Infrastructure.StaticData
{
    public interface IStaticDataService : IService
    {
        void Load();
        WeaponData ForWeapon(int id);
    }
}