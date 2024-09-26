using RoR2;
using UnityEngine;
using System.Collections.Generic;
using MSU;
namespace MSUTemplate
{
    [CreateAssetMenu(fileName = "InteractableAssetCollection", menuName = "MSUTemplateAssetCollections/InteractableAssetCollection")]
    public class InteractableAssetCollection : ExtendedAssetCollection
    {
        public GameObject interactablePrefab;
        public NullableRef<InteractableCardProvider> interactableCardProvider;
    }
}