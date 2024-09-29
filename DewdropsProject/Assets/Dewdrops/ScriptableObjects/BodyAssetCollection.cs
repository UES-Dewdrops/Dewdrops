using RoR2;
using UnityEngine;
namespace Dewdrops
{
    [CreateAssetMenu(fileName = "BodyAssetCollection", menuName = "Dewdrops/AssetCollections/BodyAssetCollection")]
    public class BodyAssetCollection : ExtendedAssetCollection
    {
        public GameObject bodyPrefab;
        public GameObject masterPrefab;
    }
}
