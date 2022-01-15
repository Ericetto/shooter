using UnityEngine;
using Code.Infrastructure.Pooling;
using Code.Infrastructure.Services.AssetProvider;
using Code.Infrastructure.Services.Input;
using Code.Infrastructure.Services.Random;
using Code.Infrastructure.StaticData;
using Code.Level.Way;
using Code.Level.Way.Follower;
using Code.Level.Way.StateMachine;
using Code.Weapon;
using Code.Weapon.TriggerMechanism;

namespace Code.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticData;
        private readonly IRandomService _random;
        private readonly IInputService _inputService;

        public GameFactory(
            ICoroutineRunner coroutineRunner,
            IAssetProvider assetProvider,
            IStaticDataService staticData,
            IRandomService random,
            IInputService inputService)
        {
            _coroutineRunner = coroutineRunner;
            _assetProvider = assetProvider;
            _staticData = staticData;
            _random = random;
            _inputService = inputService;
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

        public IWayStateMachine CreateWayStateMachine(
            IWayPoint[] wayPoints, IWayFollower wayFollower, IWayShooting wayShooting)
        {
            return new WayStateMachine(
                wayPoints,wayFollower, wayShooting, _coroutineRunner, _inputService);
        }

        private ITriggerMechanism CreateTriggerMechanism(
            GameObject gun, WeaponData weaponData)
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