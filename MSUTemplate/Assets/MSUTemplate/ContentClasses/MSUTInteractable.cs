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
    /// <inheritdoc cref="IInteractableContentPiece"/>
    /// </summary>
    public abstract class MSUTInteractable : IInteractableContentPiece, IContentPackModifier
    {
        public InteractableAssetCollection assetCollection { get; private set; }
        public InteractableCardProvider cardProvider { get; protected set; }
        public GameObject interactablePrefab { get; protected set; }

        IInteractable IGameObjectContentPiece<IInteractable>.component => interactablePrefab.GetComponent<IInteractable>();
        GameObject IContentPiece<GameObject>.asset => interactablePrefab;
        NullableRef<InteractableCardProvider> IInteractableContentPiece.cardProvider => cardProvider;

        public abstract MSUTAssetRequest<InteractableAssetCollection> LoadAssetRequest();
        public abstract void Initialize();
        public abstract bool IsAvailable(ContentPack contentPack);
        public virtual IEnumerator LoadContentAsync()
        {
            MSUTAssetRequest<InteractableAssetCollection> request = LoadAssetRequest();

            request.StartLoad();
            while (!request.isComplete)
                yield return null;

            assetCollection = request.asset;

            cardProvider = assetCollection.interactableCardProvider;
            interactablePrefab = assetCollection.interactablePrefab;

        }

        public void ModifyContentPack(ContentPack contentPack)
        {
            contentPack.AddContentFromAssetCollection(assetCollection);
        }
    }
}