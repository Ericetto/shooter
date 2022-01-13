namespace Code.Infrastructure.Services.Random
{
    public class UnityRandomService : IRandomService
    {
        public int Next(int min, int max)
        {
            return UnityEngine.Random.Range(min, max);
        }
    }
}