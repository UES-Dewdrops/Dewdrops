using RoR2;
using UnityEngine;
using System.Collections.Generic;
using RoR2.Artifacts;
using R2API.ScriptableObjects;
using MSU;

namespace MSUTemplate
{
    /// <summary>
    /// Represents an <see cref="ExtendedAssetCollection"/> that contains the assets for a <see cref="MSUTArtifact"/>
    /// </summary>
    [CreateAssetMenu(fileName = "ArtifactAssetCollection", menuName = "MSUTemplate/AssetCollections/ArtifactAssetCollection")]
    public class ArtifactAssetCollection : ExtendedAssetCollection
    {
        [Tooltip("The Artifact Code for this Artifact, can be null.")]
        public NullableRef<ArtifactCode> artifactCode;
        [Tooltip("The ArtifactDef for this Artifact, cannot be null.")]
        public ArtifactDef artifactDef;
    }
}