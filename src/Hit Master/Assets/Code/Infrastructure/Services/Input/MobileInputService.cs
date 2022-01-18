namespace Code.Infrastructure.Services.Input
{
    internal class MobileInputService : InputService
    {
        private const string FireButton = "Fire";

        public override bool IsShootButton()
        {
            return UnityEngine.Input.GetButton(FireButton) ||
                   UnityEngine.Input.GetMouseButton(0);
        }

        public override bool IsShootButtonUp()
        {
            return UnityEngine.Input.GetButtonUp(FireButton) ||
                   UnityEngine.Input.GetMouseButtonUp(0);
        }

        public override bool IsShootButtonDown()
        {
            return UnityEngine.Input.GetButtonDown(FireButton) ||
                   UnityEngine.Input.GetMouseButtonDown(0);
        }
    }
}