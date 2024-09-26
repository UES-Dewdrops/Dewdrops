using MSU;
using RoR2;
using UnityEngine;
namespace MSUTemplate
{
    [CreateAssetMenu(fileName = "SceneAssetCollection", menuName = "MSUTemplateAssetCollections/SceneAssetCollection")]
    public class SceneAssetCollection : ExtendedAssetCollection
    {
        public SceneDef sceneDef;
        public NullableRef<MusicTrackDef> mainTrackDef;
        public NullableRef<MusicTrackDef> bossTrackDef;

        [Header("Stage Registration Data")]
        public float customWeightRelativeToSiblings;
        public bool appearsPostLoop;
        public bool appearsPreLoop;
    }
}
