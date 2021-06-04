using System.Diagnostics.CodeAnalysis;
using Content.Shared.GameObjects.Components;
using Content.Shared.GameObjects.Components.Storage;
using Content.Shared.GameObjects.Components.Tag;
using JetBrains.Annotations;
using Robust.Shared.Containers;
using Robust.Shared.GameObjects;

namespace Content.Shared.GameObjects.EntitySystems
{
    [UsedImplicitly]
    public abstract class SharedStorageMapSystem : EntitySystem
    {
        /// <inheritdoc />
        public override void Initialize()
        {
            base.Initialize();

            SubscribeLocalEvent<SharedStorageMapComponent, EntInsertedIntoContainerMessage>(HandleEntityInsert);
            SubscribeLocalEvent<SharedStorageMapComponent, EntRemovedFromContainerMessage>(HandleEntityRemoved);
        }

        private void HandleEntityRemoved(EntityUid uid, SharedStorageMapComponent component,
            EntRemovedFromContainerMessage args)
        {
            UpdateSprite(args, false);
        }

        private void HandleEntityInsert(EntityUid uid, SharedStorageMapComponent component,
            EntInsertedIntoContainerMessage args)
        {
            UpdateSprite(args, true);
        }

        protected abstract void UpdateSprite(ContainerModifiedMessage args, bool show);
    }

    public static class StorageMapHelper
    {
        public static bool TryFindEntity(this SharedStorageMapComponent self, IEntity entity,
            [NotNullWhen(true)] out string? layer)
        {
            foreach (var layerProp in self._mapLayers)
            {
                var entityId = entity.Prototype?.ID;
                if (entityId != null
                    && layerProp.Id != null
                    && layerProp.Id.Contains(entityId))
                {
                    layer = layerProp._layer;
                    return true;
                }

                if (layerProp.Tags != null && entity.HasAnyTag(layerProp.Tags))
                {
                    layer = layerProp._layer;
                    return true;
                }
            }

            layer = null;
            return false;
        }
    }
}
