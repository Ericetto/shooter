using UnityEngine;
using Code.Infrastructure.Pooling;
using Code.Infrastructure.Services.AssetProvider;
using Code.Infrastructure.Services.Random;
using Code.Infrastructure.StaticData;
using Code.Weapon;

namespace Code.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticData;
        private readonly IRandomService _random;

        public GameFactory(IAssetProvider assetProvider, IStaticDataService staticData, IRandomService random)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
            _random = random;
        }

        public Gun CreateRandomGun(PoolContainer bulletPool) => 
            CreateGun(_random.Next(0, 1), bulletPool);

        public Gun CreateGun(int id, PoolContainer bulletPool)
        {
            WeaponData weaponData = _staticData.ForWeapon(id);

            GameObject weapon = _assetProvider.Instantiate(weaponData.PrefabPath);

            Gun gun = weapon.GetComponent<Gun>();
            gun.Construct(weaponData, bulletPool);
            
            return gun;
        }

        public PoolContainer CreateBulletPool(string assetPath) => 
            new PoolContainer(_assetProvider, assetPath);
    }
}