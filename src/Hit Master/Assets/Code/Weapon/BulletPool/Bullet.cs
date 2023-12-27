using UnityEngine;
using Code.Human;
using Code.Infrastructure;
using Code.Infrastructure.Pooling;

namespace Code.Weapon.BulletPool
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _startSpeed;
        [SerializeField] private float _damage;
        [SerializeField] private ParticleSystem _trail;
        
        private IPoolContainer _bloodHitFxPool;
        private IPoolContainer _environmentHitFxPool;

        private MeshRenderer _mesh;
        private bool _isCollided;

        private void Awake() => _mesh = GetComponent<MeshRenderer>();

        private void OnEnable()
        {
            _isCollided = false;
            _mesh.enabled = true;
            ActivateTrail();
        }

        private void OnDisable() => DeactivateTrail();

        private void Update()
        {
            if (!_isCollided)
                transform.Translate(Vector3.forward * (_startSpeed * Time.deltaTime));
        }

        public void Init(IPoolContainer bloodHitFxPool, IPoolContainer environmentHitFxPool)
        {
            _bloodHitFxPool = bloodHitFxPool;
            _environmentHitFxPool = environmentHitFxPool;
        }

        private void OnTriggerEnter(Collider other)
        {
            _trail.Stop();

            if (other.TryGetComponent(out HumanBodyPart humanBodyPart))
            {
                ImpactBodyPart(humanBodyPart);
                PlayBloodHitFx();
            }
            else 
            {
                if (other.TryGetComponent(out Rigidbody otherRigidbody))
                    AddForceToRigidbody(otherRigidbody);

                if (other.tag == Tags.NonThroughShootable)
                {
                    _isCollided = true;
                    _mesh.enabled = false;
                }

                PlayEnvironmentHitFx();
            }
        }

        private void ImpactBodyPart(HumanBodyPart bodyPart)
        {
            bodyPart.TakeDamage(_damage);
            AddForceToRigidbody(bodyPart.Rigidbody);
        }

        private void PlayEnvironmentHitFx()
        {
            var fx = _environmentHitFxPool.Get().transform;
            fx.position = transform.position;
        }
        
        private void ActivateTrail()   => _trail.gameObject.SetActive(true);
        private void DeactivateTrail() => _trail.gameObject.SetActive(false);

        private void PlayBloodHitFx() => _bloodHitFxPool.Get().SetTransform(transform);

        private void AddForceToRigidbody(Rigidbody rb) => rb.AddForce(
                transform.forward * _startSpeed * 50, ForceMode.Acceleration);
    }
}