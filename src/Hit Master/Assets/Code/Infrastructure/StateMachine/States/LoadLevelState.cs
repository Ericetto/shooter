using UnityEngine;
using Code.Human.Enemy;
using Code.Human.Hero;
using Code.Infrastructure.Factory;
using Code.Infrastructure.Pooling;
using Code.Infrastructure.Services.AssetProvider;
using Code.Infrastructure.Services.Input;
using Code.Logic;
using Code.Weapon;

namespace Code.Infrastructure.StateMachine.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IInputService _inputService;

        public LoadLevelState(
            GameStateMachine stateMachine,
            SceneLoader sceneLoader,
            LoadingCurtain curtain,
            IGameFactory gameFactory,
            IInputService inputService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _inputService = inputService;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnLoaded()
        {
            InitGameWorld();
            _stateMachine.Enter<GameLoopState>();
        }

        private void InitGameWorld()
        {
            PoolContainer bulletPool = CreateBulletPool();

            Transform hero = InitHero(bulletPool);

            InitEnemies(bulletPool, hero);
        }

        private Transform InitHero(PoolContainer bulletPool)
        {
            HeroShooting hero = Object.FindObjectOfType<HeroShooting>();
            hero.Construct(_inputService, CreateHeroGun(bulletPool));
            return hero.transform;
        }

        private void InitEnemies(PoolContainer bulletPool, Transform hero)
        {
            EnemyShooting[] enemies = Object.FindObjectsOfType<EnemyShooting>();

            foreach (EnemyShooting enemyShooting in enemies)
            {
                Gun gun = CreateRandomGun(bulletPool);
                enemyShooting.Construct(gun, hero);
                enemyShooting.Enable();
            }
        }

        private PoolContainer CreateBulletPool() =>
            _gameFactory.CreateBulletPool(AssetPath.Bullet);

        private Gun CreateHeroGun(PoolContainer bulletPool) =>
            CreateRandomGun(bulletPool);

        private Gun CreateRandomGun(PoolContainer bulletPool) =>
            _gameFactory.CreateRandomGun(bulletPool);
    }   
}