namespace Pooling
{
    public interface IPoolContainer
    {
        PoolObject Get();
        void Recycle(PoolObject obj);
    }
}