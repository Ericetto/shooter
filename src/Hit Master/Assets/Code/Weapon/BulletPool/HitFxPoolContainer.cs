using UnityEngine;
using System.Collections;
using Code.Infrastructure;
using Pooling;

namespace Code.Weapon.BulletPool
{
    internal class HitFxPoolContainer : PoolContainer
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly WaitForSeconds _delayWait;

        public HitFxPoolContainer(
            GameObject prefab,
            ICoroutineRunner coroutineRunner,
            Transform objectsHolder = null)
            : base(prefab, objectsHolder)
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