using UnityEngine;
using Code.Level;
using Code.Level.Way;
using Code.Human;
using Code.Human.Enemy;
using Code.Human.Hero;
using Code.Weapon;
using Code.Infrastructure.Factory;
using Code.Infrastructure.Pooling;
using Code.Infrastructure.Services.Input;
using Code.Level.Way.StateMachine;

namespace Code.Infrastructure.StateMachine.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly IStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IInputService _inputService;
        private readonly IPoolContainer _bulletPool;

        public LoadLevelState(
            IStateMachine stateMachine,
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
            ILevel level = IniLevel();
            _stateMachine.Enter<GameLoopState, ILevel>(level);
        }

        private ILevel IniLevel()
        {
            GameObject hero = InitHero();
            InitEnemies(hero);

            return new Level.Level(
                hero.GetComponent<HumanDeath>(),
                CreateWayStateMachine(hero));
        }

        private IWayStateMachine CreateWayStateMachine(GameObject hero)
        {
            return _gameFactory.CreateWayStateMachine(
                Object.FindObjectsOfType<WayPoint>(),
                hero.GetComponent<HeroWayFollower>(),
                hero.GetComponent<HeroShooting>());
        }

        private GameObject InitHero()
        {
            GameObject hero = GameObject.FindGameObjectWithTag(Tags.Player);
            hero.GetComponent<HeroShooting>().Construct(_inputService);
            hero.GetComponent<HumanEquipment>().EquipGun(CreateHeroGun());
            return hero;
        }

        private void InitEnemies(GameObject hero)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.Enemy);

            foreach (GameObject enemy in enemies)
            {
                EnemyShooting enemyShooting = enemy.GetComponent<EnemyShooting>();
                enemyShooting.Construct(hero.transform);

                Gun gun = CreateRandomGun();
                enemy.GetComponent<HumanEquipment>().EquipGun(gun);
            }
        }

        private Gun CreateHeroGun() => CreateRandomGun();

        private Gun CreateRandomGun() =>
            _gameFactory.CreateRandomGun(_bulletPool) as Gun;
    }   
}