using MSU;
using R2API;
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
    /// <inheritdoc cref="IMonsterContentPiece"/>
    /// </summary>
    public abstract class MSUTMonster : IMonsterContentPiece, IContentPackModifier
    {
        public NullableRef<MonsterCardProvider> cardProvider { get; protected set; }
        public NullableRef<DirectorCardHolderExtended> dissonanceCard { get; protected set; }
        public MonsterAssetCollection assetCollection { get; private set; }
        public NullableRef<GameObject> masterPrefab { get; protected set; }
        public GameObject characterPrefab { get; private set; }

        NullableRef<DirectorCardHolderExtended> IMonsterContentPiece.dissonanceCard => dissonanceCard;
        CharacterBody IGameObjectContentPiece<CharacterBody>.component => characterPrefab.GetComponent<CharacterBody>();
        NullableRef<MonsterCardProvider> IMonsterContentPiece.cardProvider => cardProvider;
        GameObject IContentPiece<GameObject>.asset => characterPrefab;

        public abstract MSUTAssetRequest<MonsterAssetCollection> LoadAssetRequest();
        public abstract void Initialize();
        public abstract bool IsAvailable(ContentPack contentPack);

        public virtual IEnumerator LoadContentAsync()
        {
            MSUTAssetRequest<MonsterAssetCollection> request = LoadAssetRequest();

            request.StartLoad();
            while (!request.isComplete)
                yield return null;

            assetCollection = request.asset;

            characterPrefab = assetCollection.bodyPrefab;
            masterPrefab = assetCollection.masterPrefab;
            cardProvider = assetCollection.monsterCardProvider;
        }


        public virtual void ModifyContentPack(ContentPack contentPack)
        {
            contentPack.AddContentFromAssetCollection(assetCollection);
        }
    }
}
