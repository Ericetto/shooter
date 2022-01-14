using UnityEngine;
using Code.Infrastructure.Pooling;
using Code.Infrastructure.Services.AssetProvider;
using Code.Infrastructure.Services.Random;
using Code.Infrastructure.StaticData;
using Code.Weapon;
using Code.Weapon.TriggerMechanism;

namespace Code.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticData;
        private readonly IRandomService _random;

        public GameFactory(IAssetProvider assetProvider,
            IStaticDataService staticData,
            IRandomService random)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
            _random = random;
        }

        public IGun CreateRandomGun(IPoolContainer bulletPool) => 
            CreateGun(_random.Next(0, 3), bulletPool);

        public IGun CreateGun(int id, IPoolContainer bulletPool)
        {
            WeaponData weaponData = _staticData.ForWeapon(id);

            Gun gun = _assetProvider
                .Instantiate(weaponData.PrefabPath)
                .GetComponent<Gun>();

            var triggerMechanism = CreateTriggerMechanism(gun.gameObject, weaponData);

            gun.Construct(weaponData, triggerMechanism, bulletPool);
            
            return gun;
        }

        private ITriggerMechanism CreateTriggerMechanism(GameObject gun, WeaponData weaponData)
        {
            ITriggerMechanism triggerMechanism;

            if (weaponData.IsAutomatic)
                triggerMechanism = gun.AddComponent<AutomaticTriggerMechanism>();
            else
                triggerMechanism = gun.AddComponent<SemiTriggerMechanism>();

            triggerMechanism.Construct(weaponData);

            return triggerMechanism;
        }

        public IPoolContainer CreatePool(string assetPath) =>
            new PoolContainer(_assetProvider, assetPath);
    }
}