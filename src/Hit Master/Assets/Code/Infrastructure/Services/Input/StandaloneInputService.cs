namespace Code.Infrastructure.Services.Input
{
    internal class StandaloneInputService : InputService
    {
        public override bool IsShootButton()
        {
            return UnityEngine.Input.GetMouseButton(0);
        }

        public override bool IsShootButtonUp()
        {
            return UnityEngine.Input.GetMouseButtonUp(0);
        }

        public override bool IsShootButtonDown()
        {
            return UnityEngine.Input.GetMouseButtonDown(0);
        }
    }
}