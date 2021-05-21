using System.Collections.Generic;
using Content.Shared.GameObjects.Components;
using JetBrains.Annotations;
using Robust.Client.GameObjects;
using Robust.Shared.GameObjects;
using Robust.Shared.Serialization.Manager.Attributes;
using Robust.Shared.Utility;

namespace Content.Client.GameObjects.Components
{
    [UsedImplicitly]
    public class MappedItemVisualizer : AppearanceVisualizer
    {
        [DataField("spriteLayers")] private List<string> _spriteLayers = new();
        [DataField("sprite")] private ResourcePath? _spritePath;

        public override void InitializeEntity(IEntity entity)
        {
            base.InitializeEntity(entity);

            if (entity.TryGetComponent<ISpriteComponent>(out var spriteComponent))
            {
                _spritePath ??= spriteComponent.BaseRSI!.Path!;
            }
        }

        public override void OnChangeData(AppearanceComponent component)
        {
            base.OnChangeData(component);
            InitializeSpriteMap(component);
            UpdateSprite(component);
        }

        private void InitializeSpriteMap(AppearanceComponent component)
        {
            if (component.Owner.TryGetComponent<ISpriteComponent>(out var spriteComponent)
                && component.TryGetData<List<string>>(StorageMapVisual.AllLayers, out var layers))
            {
                if (_spriteLayers.Count <= 0)
                {
                    _spriteLayers = layers;
                }

                foreach (var sprite in _spriteLayers)
                {
                    spriteComponent.LayerMapReserveBlank(sprite);
                    spriteComponent.LayerSetSprite(sprite, new SpriteSpecifier.Rsi(_spritePath!, sprite));
                    spriteComponent.LayerSetVisible(sprite, false);
                }
            }
        }

        private static void UpdateSprite(AppearanceComponent component)
        {
            if (component.Owner.TryGetComponent<ISpriteComponent>(out var spriteComp)
                && component.TryGetData<string>(StorageMapVisual.LayerName, out var layerName)
                && component.TryGetData<bool>(StorageMapVisual.Show, out var show))
            {
                spriteComp.LayerSetVisible(layerName, show);
            }
        }
    }
}
