using System;

namespace Code.Level
{
    public interface ILevel
    {
        void Start();

        event Action Finished;
        event Action Failed;
    }
}