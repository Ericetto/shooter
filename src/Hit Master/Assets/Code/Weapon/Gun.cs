using UnityEngine;
using System.Collections;
using Code.Infrastructure.Pooling;
using Code.Infrastructure.StaticData;

namespace Code.Weapon
{
    public class Gun : WeaponBase
    {
        [SerializeField] private Transform _startBulletTransform;
        [SerializeField] private float _bulletRecycleTime = 3f;
        
        private PoolContainer _bulletPool;
        private WaitForSeconds _bulletRecycleWait;
        
        public void Construct(WeaponData data, PoolContainer bulletPool)
        {
            base.Construct(data);

            _bulletPool = bulletPool;
            _bulletRecycleWait = new WaitForSeconds(_bulletRecycleTime);
        }

        public override void Attack() => Shoot();

        public void Shoot()
        {
            PoolObject bullet = _bulletPool.Get();

            bullet.transform.SetParent(null);
            bullet.transform.position = _startBulletTransform.position;
            bullet.transform.rotation = _startBulletTransform.rotation;
            bullet.SetActive(true);

            StartCoroutine(RecycleBullet(bullet));
        }

        private IEnumerator RecycleBullet(PoolObject bullet)
        {
            yield return _bulletRecycleWait;
            bullet.Recycle();
        }
    }
}