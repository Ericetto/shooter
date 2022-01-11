namespace Code.Infrastructure.Services.Random
{
    class UnityRandomService : IRandomService
    {
        public int Next(int min, int max)
        {
            return UnityEngine.Random.Range(min, max);
        }
    }
}