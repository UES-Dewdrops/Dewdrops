﻿using RoR2;
using UnityEngine;
using System.Collections.Generic;
using MSU;
namespace Dewdrops
{
    [CreateAssetMenu(fileName = "InteractableAssetCollection", menuName = "Dewdrops/AssetCollections/InteractableAssetCollection")]
    public class InteractableAssetCollection : ExtendedAssetCollection
    {
        public GameObject interactablePrefab;
        public NullableRef<InteractableCardProvider> interactableCardProvider;
    }
}