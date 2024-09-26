using MSU;
using RoR2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using RoR2.ContentManagement;

namespace MSUTemplate
{
    /// <summary>
    /// <inheritdoc cref="IItemContentPiece"/>
    /// </summary>
    public abstract class MSUTItem : IItemContentPiece, IContentPackModifier
    {
        public ItemAssetCollection assetCollection { get; private set; }
        public NullableRef<List<GameObject>> itemDisplayPrefabs { get; protected set; } = new List<GameObject>();
        public ItemDef itemDef { get; protected set; }

        ItemDef IContentPiece<ItemDef>.asset => itemDef;
        NullableRef<List<GameObject>> IItemContentPiece.itemDisplayPrefabs => itemDisplayPrefabs;

        public abstract MSUTAssetRequest LoadAssetRequest();
        public abstract void Initialize();
        public abstract bool IsAvailable(ContentPack contentPack);
        public virtual IEnumerator LoadContentAsync()
        {
            MSUTAssetRequest request = LoadAssetRequest();

            request.StartLoad();
            while (!request.isComplete)
                yield return null;

            if(request.boxedAsset is ItemAssetCollection collection)
            {
                assetCollection = collection;

                itemDef = assetCollection.itemDef;
                itemDisplayPrefabs = assetCollection.itemDisplayPrefabs;
            }
            else if(request.boxedAsset is ItemDef def)
            {
                itemDef = def;
            }
            else
            {
                MSUTLog.Error("Invalid AssetRequest " + request.assetName + " of type " + request.boxedAsset.GetType());
            }
        }

        public virtual void ModifyContentPack(ContentPack contentPack)
        {
            if(assetCollection)
                contentPack.AddContentFromAssetCollection(assetCollection);
        }
    }
}