using UnityEngine;
using System.Collections;
using Code.Infrastructure.Pooling;
using Code.Infrastructure.StaticData;
using Code.Weapon.TriggerMechanism;

namespace Code.Weapon
{
    public class Gun : MonoBehaviour, IGun
    {
        [SerializeField] private Transform _startBulletTransform;
        [SerializeField] private float _bulletRecycleTime = 3f;

        private Rigidbody _rigidbody;

        public float Damage { get; private set; }
        public bool IsPistol { get; private set; }

        private ITriggerMechanism _triggerMechanism;
        private IPoolContainer _bulletPool;
        private WaitForSeconds _bulletRecycleWait;

        protected virtual void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            SetActivePhysics(false);
        }

        public virtual void Construct(
            WeaponData data,
            ITriggerMechanism triggerMechanism,
            IPoolContainer bulletPool)
        {
            _triggerMechanism = triggerMechanism;
            _bulletPool = bulletPool;
            _bulletRecycleWait = new WaitForSeconds(_bulletRecycleTime);

            Damage = data.Damage;
            IsPistol = data.IsPistol;
        }

        public bool PullTrigger()
        {
            bool canShoot = _triggerMechanism.PullTrigger();

            if (canShoot)
                Shoot();

            return canShoot;
        }

        public void PullUpTrigger() => _triggerMechanism.PullUpTrigger();

        private void Shoot()
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