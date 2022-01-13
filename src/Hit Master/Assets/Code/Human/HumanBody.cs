using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Code.Human
{
    public class HumanBody : MonoBehaviour
    {
        [SerializeField] private HumanHealth _health;
        [SerializeField] private HumanAnimator _animator;
        [SerializeField] private HumanBodyPart[] _bodyParts;

        private void Awake()
        {
            DisablePhysics();

            foreach (var bodyPart in _bodyParts)
                bodyPart.Damaged += _health.TakeDamage;
        }

        public void EnablePhysics() => SetActivePhysics(true);
        public void DisablePhysics() => SetActivePhysics(false);

        public void AddForce(Vector3 force, ForceMode forceMode = ForceMode.Impulse)
        {
            foreach (var bodyPart in _bodyParts)
                bodyPart.Rigidbody.AddForce(force, forceMode);
        }

        public void AddExplosionForce(float force, Vector3 explosionPosition, float radius)
        {
            foreach (var bodyPart in _bodyParts)
                bodyPart.Rigidbody.AddExplosionForce(force, explosionPosition, radius);
        }

        private void SetActivePhysics(bool value)
        {
            foreach (var bodyPart in _bodyParts)
            {
                bodyPart.Rigidbody.useGravity = value;
                bodyPart.Rigidbody.isKinematic = !value;
            }

            _animator.SetActive(!value);
        }

        private void OnDestroy()
        {
            foreach (var bodyPart in _bodyParts)
                bodyPart.Damaged -= _health.TakeDamage;
        }

#if UNITY_EDITOR
        [ContextMenu("Configure bones")]
        private void ConfigureBones()
        {
            Undo.RecordObject(this, "Add HumanBodyPart components to body bones");

            var boneRigidbodies = GetComponentsInChildren<Rigidbody>();

            _bodyParts = new HumanBodyPart[boneRigidbodies.Length];

            for (var i = 0; i < boneRigidbodies.Length; i++)
            {
                var bodyPart = boneRigidbodies[i].gameObject.GetComponent<HumanBodyPart>();

                if (bodyPart == null)
                    bodyPart = boneRigidbodies[i].gameObject.AddComponent<HumanBodyPart>();

                bodyPart.Rigidbody = boneRigidbodies[i];
                _bodyParts[i] = bodyPart;
            }
        }
#endif
    }
}