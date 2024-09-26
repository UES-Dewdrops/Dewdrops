using RoR2;
using UnityEngine;
using System.Collections.Generic;
using MSU;

namespace MSUTemplate
{
    [CreateAssetMenu(fileName = "ItemAssetCollection", menuName = "MSUTemplateAssetCollections/ItemAssetCollection")]
    public class ItemAssetCollection : ExtendedAssetCollection
    {
        public NullableRef<List<GameObject>> itemDisplayPrefabs;
        public ItemDef itemDef;
    }
}