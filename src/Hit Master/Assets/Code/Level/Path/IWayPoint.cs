using Code.Level.Obstacle;
using UnityEngine;

namespace Code.Level.Path
{
    public interface IWayPoint
    {
        int          Id { get; }
        WayPointType Type { get; }
        Vector3      Position { get; }
        Vector3      LookAtPoint { get; }
        IObstacle    Obstacle { get; }
        
        void Init();
    }
}