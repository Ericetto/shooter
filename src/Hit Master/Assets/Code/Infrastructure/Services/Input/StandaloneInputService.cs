namespace Code.Infrastructure.Services.Input
{
    public class StandaloneInputService : InputService
    {
        public override bool IsShootButton()     => UnityEngine.Input.GetMouseButton(0);
        public override bool IsShootButtonUp()   => UnityEngine.Input.GetMouseButtonUp(0);
        public override bool IsShootButtonDown() => UnityEngine.Input.GetMouseButtonDown(0);
    }
}