using UnityEngine;
using Code.Infrastructure.Factory;
using Code.Infrastructure.Pooling;
using Code.Infrastructure.Services;
using Code.Infrastructure.Services.AssetProvider;
using Code.Infrastructure.Services.Input;
using Code.Infrastructure.Services.Random;
using Code.Infrastructure.StaticData;

namespace Code.Infrastructure.StateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(IStateMachine stateMachine,
            ICoroutineRunner coroutineRunner,
            SceneLoader sceneLoader,
            AllServices services)
        {
            _stateMachine = stateMachine;
            _coroutineRunner = coroutineRunner;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(SceneNames.Init, EnterLoadLevel);
        }

        public void Exit() { }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadLevelState, string>(SceneNames.Main);
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IStateMachine>(_stateMachine);
            _services.RegisterSingle<IRandomService>(new UnityRandomService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IInputService>(CreateInputService());
            _services.RegisterSingle<IStaticDataService>(CreateStaticDataService());
            _services.RegisterSingle<IPoolContainer>(CreateBulletPool());
            _services.RegisterSingle<IGameFactory>(CreateGameFactory());
        }
        private IGameFactory CreateGameFactory()
        {
            return new GameFactory(_coroutineRunner,
                _services.Single<IAssetProvider>(),
                _services.Single<IStaticDataService>(),
                _services.Single<IRandomService>(),
                _services.Single<IInputService>());
        }

        private IPoolContainer CreateBulletPool()
        {
            GameObject bulletPoolHolder = new GameObject("Bullet Holder");
            Object.DontDestroyOnLoad(bulletPoolHolder);

            return new PoolContainer(
                _services.Single<IAssetProvider>(),
                AssetPath.Bullet,
                bulletPoolHolder.transform);
        }

        private IStaticDataService CreateStaticDataService()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.Load();
            return staticData;
        }

        private static IInputService CreateInputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            
            return new MobileInputService();
        }
    }
}