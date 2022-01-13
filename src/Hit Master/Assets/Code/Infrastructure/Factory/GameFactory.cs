using Code.Infrastructure.Pooling;
using Code.Infrastructure.Services.AssetProvider;

namespace Code.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;

        public GameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public PoolContainer CreateEnemyPool() => new PoolContainer(_assetProvider, AssetPath.BulletPath);

        public PoolContainer CreateBulletPool() => new PoolContainer(_assetProvider, AssetPath.BulletPath);
    }
}