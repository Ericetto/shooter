using Code.Infrastructure.Services;
using Code.Infrastructure.StateMachine;
using Code.Logic;

namespace Code.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
        {
            StateMachine = new GameStateMachine(
                new SceneLoader(coroutineRunner),
                loadingCurtain,
                AllServices.Container);
        }
    }
}
