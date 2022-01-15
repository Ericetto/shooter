using UnityEngine;
using Code.Level.Obstacle;

namespace Code.Level.Way
{
    public interface IWayPoint
    {
        int Id { get; }
        WayPointType Type { get; }
        Vector3 Position { get; }
        Vector3 LookAtPoint { get; }
        IObstacle Obstacle { get; }
        void Init();
    }
}