using Code.Human.Mediator;

namespace Code.Human
{
    public abstract class HumanShooting : HumanComponent
    {
        private bool _isActive;

        public bool IsEnable => _isActive;
        public void Enable() => _isActive = true;
        public void Disable() => _isActive = false;

        private bool CanPullTrigger => _isActive && !Mediator.AnimatorIsInTransition;

        protected void PullTrigger()
        {
            if (CanPullTrigger && Mediator.PullTrigger())
                Mediator.AnimatorShoot();
        }

        protected void PullUpTrigger() => Mediator.PullUpTrigger();
        
        private void PullTriggerByAnimation()
        {
            if (CanPullTrigger)
                Mediator.PullTrigger();
        }
    }
}