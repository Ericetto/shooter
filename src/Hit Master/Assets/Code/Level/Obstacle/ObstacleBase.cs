using UnityEngine;
using System;

namespace Code.Level.Obstacle
{
    public abstract class ObstacleBase : MonoBehaviour, IObstacle
    {
        public virtual Vector3 LookAtPoint { get; protected set; }

        public virtual bool IsDestroyed { get; }

        public event Action Overcame;

        public virtual void Init() { }

        protected virtual void OnOvercame() => Overcame?.Invoke();
    }
}