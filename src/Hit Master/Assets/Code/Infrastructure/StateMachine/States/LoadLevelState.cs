using UnityEngine;
using Code.Level;
using Code.Level.Way;
using Code.Human;
using Code.Human.Enemy;
using Code.Human.Hero;
using Code.Weapon;
using Code.Infrastructure.Factory;
using Code.Infrastructure.Pooling;
using Code.Infrastructure.Services.AssetProvider;
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

        public LoadLevelState(
            IStateMachine stateMachine,
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
            ILevel level = IniLevel();
            _stateMachine.Enter<GameLoopState, ILevel>(level);
        }

        private ILevel IniLevel()
        {
            Transform bulletHolder = (new GameObject("Bullet Holder")).transform;
            Object.DontDestroyOnLoad(bulletHolder);

            IPoolContainer bulletPool = CreateBulletPool(bulletHolder);

            IGun heroGun = CreateRandomGun(bulletPool);
            GameObject hero = InitHero(heroGun);

            InitEnemies(bulletPool, hero);

            IWayStateMachine wayStateMachine = CreateWayStateMachine(hero);

            return new Level.Level(
                hero.GetComponent<HumanDeath>(),
                wayStateMachine);
        }

        private IPoolContainer CreateBulletPool(Transform bulletHolder)
        {
            IPoolContainer bloodHitFxPool = CreateBulletHitFxPool(
                AssetPath.BloodHitFx, bulletHolder);

            IPoolContainer environmentHitFxPool = CreateBulletHitFxPool(
                AssetPath.EnvironmentHitFx, bulletHolder);

            return CreateBulletPool(bloodHitFxPool, environmentHitFxPool, bulletHolder);
        }

        private IWayStateMachine CreateWayStateMachine(GameObject hero)
        {
            return _gameFactory.CreateWayStateMachine(
                Object.FindObjectsOfType<WayPoint>(),
                hero.GetComponent<HeroWayFollower>(),
                hero.GetComponent<HeroShooting>());
        }

        private GameObject InitHero(IGun gun)
        {
            GameObject hero = GameObject.FindGameObjectWithTag(Tags.Player);
            hero.GetComponent<HeroShooting>().Construct(_inputService);
            hero.GetComponent<HumanEquipment>().EquipGun(gun);
            return hero;
        }

        private void InitEnemies(IPoolContainer bulletPool, GameObject hero)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.Enemy);

            foreach (GameObject enemy in enemies)
            {
                EnemyShooting enemyShooting = enemy.GetComponent<EnemyShooting>();
                enemyShooting.Construct(hero.transform);

                IGun gun = CreateRandomGun(bulletPool);
                enemy.GetComponent<HumanEquipment>().EquipGun(gun);
            }
        }

        private IGun CreateRandomGun(IPoolContainer bulletPool) =>
            _gameFactory.CreateRandomGun(bulletPool);

        private IPoolContainer CreatePool(string assetPath, Transform objectsHolder) =>
            _gameFactory.CreatePool(assetPath, objectsHolder);

        private IPoolContainer CreateBulletPool(
            IPoolContainer bloodHitFxPool,
            IPoolContainer environmentHitFxPool,
            Transform bulletsHolder)
        {
            return _gameFactory.CreateBulletPool(
                AssetPath.Bullet, bloodHitFxPool, environmentHitFxPool, bulletsHolder);
        }

        private IPoolContainer CreateBulletHitFxPool(
            string assetPath, Transform bulletsHolder)
        {
            return _gameFactory.CreateBulletHitFxPool(
                assetPath, bulletsHolder);
        }
    }   
}