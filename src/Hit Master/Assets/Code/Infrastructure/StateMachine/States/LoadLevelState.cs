using UnityEngine;
using Code.Human.Enemy;
using Code.Human.Hero;
using Code.Infrastructure.Factory;
using Code.Infrastructure.Pooling;
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
        private readonly IPoolContainer _bulletPool;

        public LoadLevelState(
            GameStateMachine stateMachine,
            SceneLoader sceneLoader,
            LoadingCurtain curtain,
            IGameFactory gameFactory,
            IInputService inputService,
            IPoolContainer bulletPool)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _inputService = inputService;
            _bulletPool = bulletPool;
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
            Transform hero = InitHero();
            InitEnemies(hero);
        }

        private Transform InitHero()
        {
            GameObject hero = GameObject.FindGameObjectWithTag(Tags.Player);
            hero.GetComponent<HeroShooting>().Construct(_inputService);
            hero.GetComponent<HumanEquipment>().EquipGun(CreateHeroGun());
            return hero.transform;
        }

        private void InitEnemies(Transform hero)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.Enemy);

            foreach (GameObject enemy in enemies)
            {
                EnemyShooting enemyShooting = enemy.GetComponent<EnemyShooting>();
                enemyShooting.Construct(hero);
                enemyShooting.Enable();

                Gun gun = CreateRandomGun();
                enemy.GetComponent<HumanEquipment>().EquipGun(gun);
            }
        }

        private Gun CreateHeroGun() => CreateRandomGun();

        private Gun CreateRandomGun() =>
            _gameFactory.CreateRandomGun(_bulletPool) as Gun;
    }   
}