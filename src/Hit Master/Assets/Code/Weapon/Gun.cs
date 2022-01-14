using UnityEngine;
using System.Collections;
using Code.Infrastructure.Pooling;
using Code.Infrastructure.StaticData;

namespace Code.Weapon
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] protected Rigidbody _rigidbody;
        [SerializeField] private Transform _startBulletTransform;
        [SerializeField] private float _bulletRecycleTime = 3f;

        public float Damage { get; private set; }
        public bool IsPistol { get; private set; }

        private IPoolContainer _bulletPool;
        private WaitForSeconds _bulletRecycleWait;

        private void Start() => SetActivePhysics(false);

        public void Construct(WeaponData data, IPoolContainer bulletPool)
        {
            _bulletPool = bulletPool;
            _bulletRecycleWait = new WaitForSeconds(_bulletRecycleTime);

            Damage = data.Damage;
            IsPistol = data.IsPistol;
        }

        public void Shoot()
        {
            PoolObject bullet = _bulletPool.Get();
            
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
        public void AddForce(Vector3 force)
        {
            _rigidbody.AddForce(force, ForceMode.Acceleration);
            _rigidbody.AddTorque(force);
        }

        public void SetActivePhysics(bool value)
        {
            _rigidbody.useGravity = value;
            _rigidbody.isKinematic = !value;
        }
    }
}