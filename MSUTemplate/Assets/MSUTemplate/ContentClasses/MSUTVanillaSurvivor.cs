using MSU;
using RoR2;
using RoR2.ContentManagement;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MSUTemplate
{
    public abstract class MSUTVanillaSurvivor : IVanillaSurvivorContentPiece, IContentPackModifier
    {
        public VanillaSurvivorAssetCollection assetCollection { get; private set; }
        public SurvivorDef survivorDef { get; protected set; }

        public abstract MSUTAssetRequest<VanillaSurvivorAssetCollection> LoadAssetRequest();
        public abstract void Initialize();
        public IEnumerator InitializeAsync()
        {
            var coroutine = assetCollection.InitializeSkinDefs();
            while (!coroutine.IsDone())
                yield return null;

            yield break;
        }
        public abstract bool IsAvailable(ContentPack contentPack);
        public IEnumerator LoadContentAsync()
        {
            var assetRequest = LoadAssetRequest();

            assetRequest.StartLoad();
            while (!assetRequest.isComplete)
                yield return null;

            assetCollection = assetRequest.asset;

            var request = Addressables.LoadAssetAsync<SurvivorDef>(assetCollection.survivorDefAddress);
            while (!request.IsDone)
                yield return null;

            survivorDef = request.Result;
        }

        public virtual void ModifyContentPack(ContentPack contentPack)
        {
            contentPack.AddContentFromAssetCollection(assetCollection);
        }
    }
}