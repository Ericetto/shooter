using Code.Human;
using Code.Infrastructure.Pooling;
using UnityEngine;

namespace Code.Weapon.BulletPool
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _startSpeed;
        [SerializeField] private float _damage;

        private IPoolContainer _bloodHitFxPool;
        private IPoolContainer _environmentHitFxPool;

        private void Update() => 
            transform.Translate(Vector3.forward * _startSpeed * Time.deltaTime);

        public void Init(
            IPoolContainer bloodHitFxPool,
            IPoolContainer environmentHitFxPool)
        {
            _bloodHitFxPool = bloodHitFxPool;
            _environmentHitFxPool = environmentHitFxPool;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out HumanBodyPart humanBodyPart))
            {
                OnBodyPartTriggered(humanBodyPart);
            }
            else 
            {
                if (other.TryGetComponent(out Rigidbody otherRigidbody))
                    AddForceToRigidbody(otherRigidbody);

                PlayEnvironmentHitFx();
            }

            GetComponent<PoolObject>().Recycle();
        }

        private void OnBodyPartTriggered(HumanBodyPart bodyPart)
        {
            bodyPart.TakeDamage(_damage);
            
            AddForceToRigidbody(bodyPart.Rigidbody);

            PlayBloodHitFx();
        }

        private void PlayEnvironmentHitFx()
        {
            Transform fx = _environmentHitFxPool.Get().transform;
            fx.position = transform.position;
        }

        private void PlayBloodHitFx() =>
            _bloodHitFxPool.Get().SetTransform(transform);

        private void AddForceToRigidbody(Rigidbody rb) => rb.AddForce(
                transform.forward * _startSpeed * 50, ForceMode.Acceleration);
    }
}