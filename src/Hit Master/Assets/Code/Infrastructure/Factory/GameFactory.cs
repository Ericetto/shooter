using UnityEngine;
using Code.Infrastructure.Services.Input;
using Code.Infrastructure.Services.Random;
using Code.Infrastructure.StaticData;
using Code.Level.Way;
using Code.Level.Way.Follower;
using Code.Level.Way.StateMachine;
using Code.Weapon;
using Code.Weapon.BulletPool;
using Code.Weapon.TriggerMechanism;
using Pooling;
using AssetProvider;

namespace Code.Infrastructure.Factory
{
    internal class GameFactory : IGameFactory
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

        public IWayStateMachine CreateWayStateMachine(
            IWayPoint[] wayPoints, IWayFollower wayFollower, IWayShooting wayShooting)
        {
            return new WayStateMachine(
                wayPoints,wayFollower, wayShooting, _coroutineRunner, _inputService);
        }

        public IGun CreateRandomGun(IPoolContainer bulletPool) => 
            CreateGun(_random.Next(0, 3), bulletPool);

        public IGun CreateGun(int id, IPoolContainer bulletPool)
        {
            WeaponData weaponData = _staticData.ForWeapon(id);

            Gun gun = _assetProvider
                .Instantiate(weaponData.PrefabPath)
                .GetComponent<Gun>();

            var triggerMechanism = CreateTriggerMechanism(weaponData);
            triggerMechanism.SetParent(gun.transform);

            gun.Construct(weaponData, triggerMechanism, bulletPool);
            
            return gun;
        }

        private ITriggerMechanism CreateTriggerMechanism(WeaponData weaponData)
        {
            GameObject trigger = new GameObject("Trigger");

            ITriggerMechanism triggerMechanism;

            if (weaponData.IsAutomatic)
                triggerMechanism = trigger.AddComponent<AutomaticTriggerMechanism>();
            else
                triggerMechanism = trigger.AddComponent<SemiTriggerMechanism>();

            triggerMechanism.Construct(weaponData);

            return triggerMechanism;
        }

        public IPoolContainer CreatePool(
            GameObject prefab, Transform objectsHolder)
        {
            return new PoolContainer(
                _assetProvider.Load(AssetPath.Bullet), objectsHolder);
        }

        public IPoolContainer CreateBulletPool(
            IPoolContainer bloodHitFxPool,
            IPoolContainer environmentHitFxPool,
            Transform bulletsHolder)
        {
            return new BulletPoolContainer(
                _assetProvider.Load(AssetPath.Bullet),
                _coroutineRunner,
                bloodHitFxPool,
                environmentHitFxPool,
                bulletsHolder);
        }

        public IPoolContainer CreateBulletHitFxPool(
            string assetPath, Transform bulletsHolder)
        {
            return new HitFxPoolContainer(
                _assetProvider.Load(assetPath), _coroutineRunner, bulletsHolder);
        }
    }
}