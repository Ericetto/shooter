using System;
using UnityEngine;

namespace Code.Level.Path.Follower
{
    public interface IPathFollower
    {
        event Action PointReached;
        void SetPoint(IWayPoint wayPoint);
        void SetLookTarget(Vector3 point);
    }
}