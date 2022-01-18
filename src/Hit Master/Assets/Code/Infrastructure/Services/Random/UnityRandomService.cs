namespace Code.Infrastructure.Services.Random
{
    internal class UnityRandomService : IRandomService
    {
        public int Next(int minInclusive, int maxInclusive) => 
            UnityEngine.Random.Range(minInclusive, maxInclusive + 1);
    }
}