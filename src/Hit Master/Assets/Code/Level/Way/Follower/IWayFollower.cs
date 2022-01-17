using UnityEngine;
using System;

namespace Code.Level.Way.Follower
{
    public interface IWayFollower
    {
        event Action PointReached;
        void SetPoint(IWayPoint wayPoint);
        void SetLookTarget(Vector3 point);
    }
}