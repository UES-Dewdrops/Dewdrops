using RoR2;
using UnityEngine;
namespace Dewdrops
{
    [CreateAssetMenu(fileName = "BodyAssetCollection", menuName = "DewdropsAssetCollections/BodyAssetCollection")]
    public class BodyAssetCollection : ExtendedAssetCollection
    {
        public GameObject bodyPrefab;
        public GameObject masterPrefab;
    }
}
