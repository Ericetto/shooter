using Code.Infrastructure.Pooling;
using Code.Infrastructure.Services.AssetProvider;
using Code.Infrastructure.StaticData;
using UnityEngine;

namespace Code.Weapon.BulletPool
{
    public class BulletPoolContainer : PoolContainer
    {
        private readonly WeaponData _weaponData;
        private readonly IPoolContainer _bloodHitFxPool;
        private readonly IPoolContainer _environmentHitFxPool;

        public BulletPoolContainer(
            IAssetProvider assetProvider,
            string prefabPath,
            IPoolContainer bloodHitFxPool,
            IPoolContainer environmentHitFxPool,
            Transform objectsHolder = null)
            : base(assetProvider, prefabPath, objectsHolder)
        {
            _bloodHitFxPool = bloodHitFxPool;
            _environmentHitFxPool = environmentHitFxPool;
        }

        protected override void InitObject(PoolObject obj)
        {
            obj.GetComponent<Bullet>().Init(
                _bloodHitFxPool, _environmentHitFxPool);
        }
    }
}