using UnityEngine;
using Code.Level.Obstacle;

namespace Code.Level.Way
{
    internal class WayPoint : MonoBehaviour, IWayPoint
    {
        [SerializeField] private int _id;
        [SerializeField] private WayPointType _type;
        [SerializeField] private ObstacleBase _obstacle;

        public int Id => _id;

        public Vector3 Position => transform.position;

        public Vector3 LookAtPoint => _obstacle != null ?
            _obstacle.LookAtPoint : transform.position;

        public WayPointType Type => _type;

        public IObstacle Obstacle => _obstacle;

        public void Init()
        {
            if (_obstacle != null)
                _obstacle.Init();
        }
    }
}