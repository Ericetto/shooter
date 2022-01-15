using UnityEngine;
using Code.Infrastructure.StateMachine.States;

namespace Code.Infrastructure
{
    public class GameBootstrapper : Singleton<GameBootstrapper>, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain _curtainPrefab;

        private Game _game;

        protected override void Init()
        {
            _game = new Game(this, Instantiate(_curtainPrefab));
            _game.StateMachine.Enter<BootstrapState>();
        }
    }
}
