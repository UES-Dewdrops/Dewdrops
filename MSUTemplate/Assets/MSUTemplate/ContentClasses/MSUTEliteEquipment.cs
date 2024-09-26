﻿using MSU;
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
    /// <inheritdoc cref="IEliteContentPiece"/>
    /// </summary>
    public abstract class MSUTEliteEquipment : IEliteContentPiece, IContentPackModifier
    {
        /// <summary>
        /// <inheritdoc cref="IEliteContentPiece.EliteDefs"/>
        /// </summary>
        public List<EliteDef> eliteDefs { get; protected set; }
        /// <summary>
        /// The EliteAssetCollection for this Elite. Populated when the Elite's assets loads, cannot be null.
        /// </summary>
        public EliteAssetCollection assetCollection { get; private set; }
        public NullableRef<List<GameObject>> itemDisplayPrefabs { get; protected set; } = new List<GameObject>();
        public EquipmentDef equipmentDef { get; protected set; }

        List<EliteDef> IEliteContentPiece.eliteDefs => eliteDefs;
        NullableRef<List<GameObject>> IEquipmentContentPiece.itemDisplayPrefabs => itemDisplayPrefabs;
        EquipmentDef IContentPiece<EquipmentDef>.asset => equipmentDef;


        /// <summary>
        /// Method for loading an AssetRequest for this class. This will later get loaded Asynchronously.
        /// </summary>
        /// <returns>An MSUTAssetRequest that loads an EliteAssetCollection</returns>
        public abstract MSUTAssetRequest<EliteAssetCollection> LoadAssetRequest();
        public abstract void Initialize();
        public abstract bool IsAvailable(ContentPack contentPack);
        public virtual IEnumerator LoadContentAsync()
        {
            MSUTAssetRequest<EliteAssetCollection> request = LoadAssetRequest();

            request.StartLoad();
            while (!request.isComplete)
                yield return null;

            assetCollection = request.asset;

            eliteDefs = assetCollection.eliteDefs;
            equipmentDef = assetCollection.equipmentDef;
            itemDisplayPrefabs = assetCollection.itemDisplayPrefabs;
        }


        //If an asset collection was loaded, the asset collection will be added to your mod's ContentPack.
        public virtual void ModifyContentPack(ContentPack contentPack)
        {
            contentPack.AddContentFromAssetCollection(assetCollection);
        }

        public abstract bool Execute(EquipmentSlot slot);
        public abstract void OnEquipmentLost(CharacterBody body);
        public abstract void OnEquipmentObtained(CharacterBody body);
    }
}