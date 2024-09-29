using RoR2;
using UnityEngine;
using System.Collections.Generic;
namespace Dewdrops
{
    [CreateAssetMenu(fileName = "EliteAssetCollection", menuName = "DewdropsAssetCollections/EliteAssetCollection")]
    public class EliteAssetCollection : EquipmentAssetCollection
    {
        public List<EliteDef> eliteDefs;
    }
}