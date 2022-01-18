using Code.Infrastructure.ServiceLocator;
using Code.Infrastructure.StateMachine;

namespace Code.Infrastructure
{
    internal class Game
    {
        public GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
        {
            StateMachine = new GameStateMachine(
                new SceneLoader(coroutineRunner),
                coroutineRunner,
                loadingCurtain,
                new AllServices());
        }
    }
}
