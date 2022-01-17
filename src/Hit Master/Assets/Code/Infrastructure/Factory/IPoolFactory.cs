using UnityEngine;
using Code.Infrastructure.Pooling;

namespace Code.Infrastructure.Factory
{
    public interface IPoolFactory
    {
        IPoolContainer CreatePool(
            string assetPath, Transform objectsHolder);

        IPoolContainer CreateBulletPool(
            string bulletAssetPath,
            IPoolContainer bloodHitFxPool,
            IPoolContainer environmentHitFxPool,
            Transform objectsHolder);

        IPoolContainer CreateBulletHitFxPool(
            string hitFxAssetPath,
            Transform objectsHolder);
    }
}