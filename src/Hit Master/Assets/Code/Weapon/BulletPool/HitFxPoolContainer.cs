using System.Collections;
using Code.Infrastructure;
using Code.Infrastructure.Pooling;
using Code.Infrastructure.Services.AssetProvider;
using UnityEngine;

namespace Code.Weapon.BulletPool
{
    public class HitFxPoolContainer : PoolContainer
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly WaitForSeconds _delayWait;

        public HitFxPoolContainer(
            IAssetProvider assetProvider,
            string prefabPath,
            ICoroutineRunner coroutineRunner,
            Transform objectsHolder = null)
            : base(assetProvider, prefabPath, objectsHolder)
        {
            _coroutineRunner = coroutineRunner;
            _delayWait = new WaitForSeconds(2f);
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