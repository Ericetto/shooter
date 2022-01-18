using UnityEngine;

namespace Code.Human.Mediator
{
    [RequireComponent(typeof(HumanMediator))]
    internal class HumanComponent : MonoBehaviour
    {
        protected HumanMediator Mediator { get; private set; }

        private void Awake()
        {
            Mediator = GetComponent<HumanMediator>();
            OnAwake();
        }

        protected virtual void OnAwake() { }
    }
}