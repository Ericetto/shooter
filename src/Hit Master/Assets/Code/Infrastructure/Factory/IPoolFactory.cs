using UnityEngine;
using Pooling;

namespace Code.Infrastructure.Factory
{
    public interface IPoolFactory
    {
        IPoolContainer CreatePool(
            GameObject prefab, Transform objectsHolder);

        IPoolContainer CreateBulletPool(
            IPoolContainer bloodHitFxPool,
            IPoolContainer environmentHitFxPool,
            Transform objectsHolder);

        IPoolContainer CreateBulletHitFxPool(
            string assetPath, Transform bulletsHolder);
    }
}