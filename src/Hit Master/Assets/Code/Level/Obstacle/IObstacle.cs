using UnityEngine;
using System;

namespace Code.Level.Obstacle
{
    public interface IObstacle
    {
        Vector3 LookAtPoint { get; }

        bool IsDestroyed { get; }

        event Action Overcame;

        void Init();
    }
}