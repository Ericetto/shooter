using UnityEngine;

namespace Code.Weapon
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _startBulletTransform;

        private void Start()
        {
            SetActivePhysics(false);
        }

        public void Shoot()
        {
            var bullet = Instantiate(
                _bulletPrefab,
                _startBulletTransform.position,
                _startBulletTransform.rotation);

            Destroy(bullet.gameObject, 3f);
        }

        public void SetActivePhysics(bool value)
        {
            _rigidbody.useGravity = value;
            _rigidbody.isKinematic = !value;

            GetComponent<Collider>().enabled = value;
        }

        public void AddForce(Vector3 force)
        {
            _rigidbody.AddForce(force, ForceMode.Acceleration);
            _rigidbody.AddTorque(new Vector3(0, 0, 300));
        }
    }
}