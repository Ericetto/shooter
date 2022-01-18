using System;

namespace Code.Level
{
    internal interface ILevel
    {
        void Start();

        event Action Finished;
        event Action Failed;
    }
}