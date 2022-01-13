namespace Code.Infrastructure.Pooling
{
    public interface IPoolContainer
    {
        PoolObject Get();
        void Recycle(PoolObject obj);
    }
}