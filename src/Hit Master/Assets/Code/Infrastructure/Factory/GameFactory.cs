﻿using UnityEngine;
using Code.Infrastructure.Pooling;
using Code.Infrastructure.Services.AssetProvider;
using Code.Infrastructure.Services.Input;
using Code.Infrastructure.Services.Random;
using Code.Infrastructure.StaticData;
using Code.Level.Path;
using Code.Level.Path.Follower;
using Code.Level.Path.StateMachine;
using Code.Weapon;
using Code.Weapon.BulletPool;
using Code.Weapon.TriggerMechanism;

namespace Code.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly ICoroutineRunner   _coroutineRunner;
        private readonly IAssetProvider     _assetProvider;
        private readonly IStaticDataService _staticData;
        private readonly IInputService      _inputService;
        private readonly IRandomService     _random;

        public GameFactory(
            ICoroutineRunner   coroutineRunner,
            IAssetProvider     assetProvider,
            IStaticDataService staticData,
            IRandomService     random,
            IInputService      inputService)
        {
            _coroutineRunner = coroutineRunner;
            _assetProvider   = assetProvider;
            _staticData      = staticData;
            _inputService    = inputService;
            _random          = random;
        }

        public IPathStateMachine CreateWayStateMachine(
            IWayPoint[] wayPoints, IPathFollower pathFollower, IPathShooting pathShooting)
        {
            return new PathStateMachine(
                wayPoints,pathFollower, pathShooting, _coroutineRunner, _inputService);
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
            string assetPath, Transform objectsHolder)
        {
            return new PoolContainer(
                _assetProvider, assetPath, objectsHolder);
        }

        public IPoolContainer CreateBulletPool(
            string bulletAssetPath,
            IPoolContainer bloodHitFxPool,
            IPoolContainer environmentHitFxPool,
            Transform bulletsHolder)
        {
            return new BulletPoolContainer(
                _assetProvider,
                bulletAssetPath,
                bloodHitFxPool,
                environmentHitFxPool,
                bulletsHolder);
        }

        public IPoolContainer CreateBulletHitFxPool(
            string assetPath,
            Transform bulletsHolder)
        {
            return new HitFxPoolContainer(
                _assetProvider,
                assetPath,
                _coroutineRunner,
                bulletsHolder);
        }
    }
}