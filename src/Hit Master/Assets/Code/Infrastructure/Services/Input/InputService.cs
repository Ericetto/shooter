namespace Code.Infrastructure.Services.Input
{
    internal abstract class InputService : IInputService
    {
        public abstract bool IsShootButton();
        public abstract bool IsShootButtonUp();
        public abstract bool IsShootButtonDown();
    }
}
