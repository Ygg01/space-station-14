using System;
using Robust.Shared.Serialization;

namespace Content.Shared.GameObjects.Components
{
    [Serializable, NetSerializable]
    public enum StorageMapVisual : byte
    {
        /// <summary>
        /// The name of layer being updated
        /// </summary>
        LayerName,
        /// <summary>
        /// Whether to show it or hide it
        /// </summary>
        Show,
        AllLayers
    }
}
