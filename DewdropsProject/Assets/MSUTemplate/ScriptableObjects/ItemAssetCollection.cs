using RoR2;
using UnityEngine;
using System.Collections.Generic;
using MSU;

namespace Dewdrops
{
    [CreateAssetMenu(fileName = "ItemAssetCollection", menuName = "DewdropsAssetCollections/ItemAssetCollection")]
    public class ItemAssetCollection : ExtendedAssetCollection
    {
        public NullableRef<List<GameObject>> itemDisplayPrefabs;
        public ItemDef itemDef;
    }
}