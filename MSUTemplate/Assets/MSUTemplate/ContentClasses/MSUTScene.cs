using MSU;
using R2API.ScriptableObjects;
using RoR2;
using RoR2.ContentManagement;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
namespace MSUTemplate
{
    public abstract class MSUTScene : ISceneContentPiece, IContentPackModifier
    {
        public SceneAssetCollection assetCollection { get; private set; }
        public NullableRef<MusicTrackDef> mainTrack { get; protected set; }
        public NullableRef<MusicTrackDef> bossTrack { get; protected set; }
        public NullableRef<Texture2D> bazaarTextureBase { get; protected set; } // ???
        public SceneDef asset { get; protected set; }
        public float? weightRelativeToSiblings { get; protected set; }
        public bool? preLoop { get; protected set; }
        public bool? postLoop { get; protected set; }

        public abstract MSUTAssetRequest<SceneAssetCollection> LoadAssetRequest();
        public abstract void Initialize();
        public abstract bool IsAvailable(ContentPack contentPack);
        public virtual IEnumerator LoadContentAsync()
        {
            MSUTAssetRequest<SceneAssetCollection> request = LoadAssetRequest();

            request.StartLoad();
            while (!request.isComplete)
                yield return null;

            assetCollection = request.asset;

            asset = assetCollection.sceneDef;
            mainTrack = assetCollection.mainTrackDef;
            bossTrack = assetCollection.bossTrackDef;
            weightRelativeToSiblings = assetCollection.customWeightRelativeToSiblings;
            postLoop = assetCollection.appearsPostLoop;
            preLoop = assetCollection.appearsPreLoop;
        }


        public virtual void ModifyContentPack(ContentPack contentPack)
        {
            contentPack.AddContentFromAssetCollection(assetCollection);
        }

        public virtual void OnServerStageComplete(Stage stage)
        {
        }

        public virtual void OnServerStageBegin(Stage stage)
        {           
        }
    }
}
