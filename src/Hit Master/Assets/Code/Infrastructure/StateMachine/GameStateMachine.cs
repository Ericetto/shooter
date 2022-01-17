using System;
using System.Collections.Generic;
using Code.Infrastructure.Factory;
using Code.Infrastructure.Services;
using Code.Infrastructure.Services.Input;
using Code.Infrastructure.StateMachine.States;

namespace Code.Infrastructure.StateMachine
{
    public class GameStateMachine : StateMachineBase
    {
        public GameStateMachine(SceneLoader sceneLoader,
            ICoroutineRunner coroutineRunner,
            LoadingCurtain loadingCurtain,
            AllServices services)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this,
                    coroutineRunner,
                    sceneLoader,
                    services),

                [typeof(LoadLevelState)] = new LoadLevelState(this,
                    sceneLoader,
                    loadingCurtain,
                    services.Single<IGameFactory>(),
                    services.Single<IInputService>()),

                [typeof(GameLoopState)] = new GameLoopState(this)
            };
        }
    }
}
