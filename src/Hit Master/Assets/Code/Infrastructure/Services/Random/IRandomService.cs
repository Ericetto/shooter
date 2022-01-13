namespace Code.Infrastructure.Services.Random
{
    public interface IRandomService : IService
    {
        int Next(int minInclusive, int maxInclusive);
    }
}