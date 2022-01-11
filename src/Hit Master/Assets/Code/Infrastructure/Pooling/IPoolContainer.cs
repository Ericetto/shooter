namespace Code.Infrastructure.Services.Pooling
{
    public interface IPoolContainer
    {
        PoolObject Get();
        void Recycle(PoolObject obj);
    }
}