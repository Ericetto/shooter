using UnityEngine;
using Code.Human;
using Code.Human.Enemy;
using System.Linq;

namespace Code.Level.Obstacle
{
    public class EnemyObstacle : ObstacleBase
    {
        [SerializeField] private HumanHealth[] _enemies;

        public override Vector3 LookAtPoint => CalcLookAtPoint();

        public override void Init() => InitEnemies();

        private void InitEnemies()
        {
            foreach (HumanHealth enemy in _enemies)
            {
                if (!enemy.IsAlive)
                    continue;

                enemy.GetComponent<HumanDeath>().Happened += OnEnemyDead;

                EnemyShooting shooting = enemy.GetComponent<EnemyShooting>();

                if (shooting != null)
                    shooting.Enable();
            }
        }

        private Vector3 CalcLookAtPoint()
        {
            int aliveCount = AliveCount();

            if (aliveCount == 0)
                return transform.position + Vector3.up;

            Vector3 lookAtPoint = Vector3.zero;

            foreach (HumanHealth enemy in _enemies)
                if (enemy.IsAlive)
                    lookAtPoint += enemy.transform.position;

            return lookAtPoint / aliveCount + Vector3.up;
        }

        private void OnEnemyDead()
        {
            if (AliveCount() == 0)
                OnOvercame();
        }

        private int AliveCount() => _enemies.Count(x => x.IsAlive);
    }
}