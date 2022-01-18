using Code.Infrastructure.ServiceLocator;

namespace Code.Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        bool IsShootButton();
        bool IsShootButtonUp();
        bool IsShootButtonDown();
    }
}
