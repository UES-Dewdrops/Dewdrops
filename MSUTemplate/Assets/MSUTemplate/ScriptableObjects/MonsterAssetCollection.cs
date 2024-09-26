using RoR2;
using UnityEngine;
using MSU;
using System.Collections.Generic;
namespace MSUTemplate
{
    [CreateAssetMenu(fileName = "MonsterAssetCollection", menuName = "MSUTemplateAssetCollections/MonsterAssetCollection")]
    public class MonsterAssetCollection : BodyAssetCollection
    {
        public MonsterCardProvider monsterCardProvider;
    }
}