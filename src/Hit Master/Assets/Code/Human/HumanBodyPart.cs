using UnityEngine;
using System;

namespace Code.Human
{
    [RequireComponent(typeof(Collider))]
    public class HumanBodyPart : MonoBehaviour
    {
        [SerializeField] private float _damageModifier = 1;

        public Rigidbody Rigidbody;

        public event Action<float> Damaged;

        public void TakeDamage(float damage) => 
            Damaged?.Invoke(damage * _damageModifier);
    }
}