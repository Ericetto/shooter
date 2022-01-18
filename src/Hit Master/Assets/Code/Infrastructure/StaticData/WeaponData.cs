using UnityEngine;

namespace Code.Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "Weapon Data", menuName = "Weapon Data")]
    public class WeaponData : ScriptableObject
    {
        public int Id;
        public int Damage;
        public bool IsAutomatic;
        public float FireRate;
        public bool IsPistol;
        public GameObject Prefab;
        public string PrefabPath => AssetPath.WeaponFolder + Prefab.name;
    }
}