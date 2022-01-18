using UnityEngine;
using System;

namespace Code.Level.Obstacle
{
    internal abstract class ObstacleBase : MonoBehaviour, IObstacle
    {
        public virtual Vector3 LookAtPoint { get; protected set; }

        public event Action Overcame;

        public virtual void Init() { }

        protected virtual void OnOvercame() => Overcame?.Invoke();
    }
}