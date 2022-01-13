using System.Collections;
using UnityEngine;
using Code.Human;

namespace Code.Weapon
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private float _startSpeed;
        [SerializeField] private float _damage;
        [SerializeField] private ParticleSystem _bloodHitFx;
        [SerializeField] private ParticleSystem _environmentHitFx;

        private float _speed;

        private void OnEnable()
        {
            _collider.enabled = true;
            _speed = _startSpeed;
        }

        private void Update()
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out HumanBodyPart humanBodyPart))
            {
                humanBodyPart.TakeDamage(_damage);

                humanBodyPart.Rigidbody.AddForce(
                    transform.forward * _startSpeed * 50,
                    ForceMode.Acceleration);

                transform.SetParent(humanBodyPart.transform);

                _bloodHitFx.Play();
            }
            else
            {
                if (other.TryGetComponent(out Rigidbody otherRigidbody))
                {
                    otherRigidbody.AddForce(
                        transform.forward * _startSpeed * 50,
                        ForceMode.Acceleration);
                }

                _environmentHitFx.Play();
            }

            _collider.enabled = false;
            _speed = 0;
        }
    }
}