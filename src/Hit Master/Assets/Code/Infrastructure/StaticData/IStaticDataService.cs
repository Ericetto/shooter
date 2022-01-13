using Code.Infrastructure.Services;

namespace Code.Infrastructure.StaticData
{
    public interface IStaticDataService : IService
    {
        void Load();
        WeaponData ForWeapon(int id);
    }
}