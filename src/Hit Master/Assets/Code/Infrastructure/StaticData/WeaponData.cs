using Code.Infrastructure.Services.AssetProvider;
using UnityEngine;

namespace Code.Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "Weapon Data", menuName = "Weapon Data")]
    public class WeaponData : ScriptableObject
    {
        public int Id;
        public int Damage;
        public GameObject Prefab;
        public string PrefabPath => AssetPath.WeaponFolder + Prefab.name;
    }
}