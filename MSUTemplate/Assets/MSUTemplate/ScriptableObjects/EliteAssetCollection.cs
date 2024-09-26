using RoR2;
using UnityEngine;
using System.Collections.Generic;
namespace MSUTemplate
{
    [CreateAssetMenu(fileName = "EliteAssetCollection", menuName = "MSUTemplateAssetCollections/EliteAssetCollection")]
    public class EliteAssetCollection : EquipmentAssetCollection
    {
        public List<EliteDef> eliteDefs;
    }
}