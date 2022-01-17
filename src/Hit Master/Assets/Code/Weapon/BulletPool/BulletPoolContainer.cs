using System.Collections;
using Code.Infrastructure;
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
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly WaitForSeconds _delayWait;

        public BulletPoolContainer(
            IAssetProvider assetProvider,
            string prefabPath,
            ICoroutineRunner coroutineRunner,
            IPoolContainer bloodHitFxPool,
            IPoolContainer environmentHitFxPool,
            Transform objectsHolder = null)
            : base(assetProvider, prefabPath, objectsHolder)
        {
            _bloodHitFxPool = bloodHitFxPool;
            _environmentHitFxPool = environmentHitFxPool;
            _coroutineRunner = coroutineRunner;
            _delayWait = new WaitForSeconds(3);
        }

        protected override void InitObject(PoolObject obj)
        {
            obj.GetComponent<Bullet>().Init(
                _bloodHitFxPool, _environmentHitFxPool);
        }

        protected override void OnGetting(PoolObject obj)
        {
            obj.SetActive(true);
            _coroutineRunner.StartCoroutine(DelayRecycle(obj));
        }

        private IEnumerator DelayRecycle(PoolObject obj)
        {
            yield return _delayWait;
            obj.Recycle();
        }
    }
}