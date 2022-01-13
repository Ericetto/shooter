using System.Collections.Generic;
using System.Linq;
using Code.Infrastructure.Services.AssetProvider;
using UnityEngine;

namespace Code.Infrastructure.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<int, WeaponData> _weapon;

        public void Load()
        {
            _weapon = Resources
                .LoadAll<WeaponData>(AssetPath.WeaponDataFolder)
                .ToDictionary(x => x.Id, x => x);
        }

        public WeaponData ForWeapon(int id) =>
            _weapon.TryGetValue(id, out WeaponData data) ? data : null;
    }
}