using RoR2;
using UnityEngine;
using System.Collections.Generic;
using MSU;

namespace Dewdrops
{
    [CreateAssetMenu(fileName = "EquipmentAssetCollection", menuName = "Dewdrops/AssetCollections/EquipmentAssetCollection")]
    public class EquipmentAssetCollection : ExtendedAssetCollection
    {
        public NullableRef<List<GameObject>> itemDisplayPrefabs;
        public EquipmentDef equipmentDef;
    }
}