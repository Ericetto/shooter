namespace Code.Infrastructure.Services.Random
{
    public class UnityRandomService : IRandomService
    {
        public int Next(int minInclusive, int maxInclusive) => 
            UnityEngine.Random.Range(minInclusive, maxInclusive + 1);
    }
}