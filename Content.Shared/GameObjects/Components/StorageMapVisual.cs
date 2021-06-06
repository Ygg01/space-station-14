using System;
using Robust.Shared.Serialization;

namespace Content.Shared.GameObjects.Components
{
    [Serializable, NetSerializable]
    public enum StorageMapVisual : sbyte
    {
        /// <summary>
        /// The name of layer being updated
        /// </summary>
        LayerName  = 100,
        /// <summary>
        /// Whether to show it or hide it
        /// </summary>
        Show = 101,
        AllLayers = 110
    }
}
