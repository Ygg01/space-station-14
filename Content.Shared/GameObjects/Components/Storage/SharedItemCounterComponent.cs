using System;
using System.Collections.Generic;
using Robust.Shared.GameObjects;
using Robust.Shared.Serialization;
using Robust.Shared.Serialization.Manager.Attributes;

namespace Content.Shared.GameObjects.Components.Storage
{
    public abstract class SharedItemCounterComponent : Component, ISerializationHooks
    {
        public override string Name => "ItemCounter";

        [DataField("countTag")] public string? _countTag;
        [DataField("amount")] private int? _maxAmount;

        [DataField("mapLayers")] public readonly List<LayerProperties> _mapLayers = new();
        public IReadOnlyList<string> SpriteLayers = new List<string>();

        [Serializable]
        [DataDefinition]
        public struct LayerProperties
        {
            [DataField("layer")] public string Layer;
            [DataField("ids")] public List<string>? Id { get; set; }
            [DataField("tags")] public List<string>? Tags { get; set; }
        }

        void ISerializationHooks.AfterDeserialization()
        {
            var allLayers = new List<string>();
            foreach (var mapLayer in _mapLayers)
            {
                if (!allLayers.Contains(mapLayer.Layer))
                {
                    allLayers.Add(mapLayer.Layer);
                }
            }

            SpriteLayers = allLayers;
        }

        public override void Initialize()
        {
            base.Initialize();

            if (!Owner.TryGetComponent(out SharedAppearanceComponent? appearance))
                return;
            if (SpriteLayers.Count > 0)
            {
                appearance.SetData(StorageMapVisual.AllLayers, SpriteLayers);
            }

            if (_maxAmount != null)
            {
                appearance.SetData(StackVisuals.MaxCount, _maxAmount);
            }
        }
    }
}
