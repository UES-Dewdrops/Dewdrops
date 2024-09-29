using RoR2;
using UnityEngine;
using MSU;
using System.Collections.Generic;
namespace Dewdrops
{
    [CreateAssetMenu(fileName = "MonsterAssetCollection", menuName = "DewdropsAssetCollections/MonsterAssetCollection")]
    public class MonsterAssetCollection : BodyAssetCollection
    {
        public MonsterCardProvider monsterCardProvider;
    }
}