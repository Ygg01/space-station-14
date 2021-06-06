using System.Collections.Generic;
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
    public class SharedItemCounterSystem : EntitySystem
    {
        /// <inheritdoc />
        public override void Initialize()
        {
            base.Initialize();
            SubscribeLocalEvent<SharedItemCounterComponent, ComponentStartup>(OnItemCounterStart);
            SubscribeLocalEvent<SharedItemCounterComponent, EntInsertedIntoContainerMessage>(HandleEntityInsert);
            SubscribeLocalEvent<SharedItemCounterComponent, EntRemovedFromContainerMessage>(HandleEntityRemoved);
        }

        private void OnItemCounterStart(EntityUid uid, SharedItemCounterComponent counterComponent, ComponentStartup args)
        {
            if (!ComponentManager.TryGetComponent(uid, out SharedAppearanceComponent? appearance))
                return;

            if (counterComponent.HasItemMap())
            {
            }

            if (counterComponent.HasTagCount())
            {
                appearance.SetData(StackVisuals.Actual, counterComponent.Count());
            }
        }

        private void HandleEntityRemoved(EntityUid uid, SharedItemCounterComponent counterComponent,
            EntRemovedFromContainerMessage args)
        {
            UpdateSprite(args, false);
        }

        private void HandleEntityInsert(EntityUid uid, SharedItemCounterComponent counterComponent,
            EntInsertedIntoContainerMessage args)
        {
            UpdateSprite(args, true);
        }


        protected void UpdateSprite(ContainerModifiedMessage args, bool show)
        {
            if (args.Container.Owner.TryGetComponent(out SharedItemCounterComponent? storageMapComponent)
                && args.Container.Owner.TryGetComponent(out SharedAppearanceComponent? appearanceComponent))
            {
                if (storageMapComponent.HasItemMap()
                    && storageMapComponent.TryFindEntity(args.Entity, out var layer))
                {
                    appearanceComponent.SetData(StorageMapVisual.LayerName, layer);
                    appearanceComponent.SetData(StorageMapVisual.Show, show);
                }

                if (storageMapComponent.HasTagCount()
                    && args.Entity.HasTag(storageMapComponent._countTag!))
                {
                    appearanceComponent.SetData(StackVisuals.Actual, 3);
                }
            }
        }
    }

    internal static class StorageMapHelper
    {
        public static bool TryFindEntity(this SharedItemCounterComponent self, IEntity entity,
            [NotNullWhen(true)] out string? layer)
        {
            foreach (var layerProp in self._mapLayers)
            {
                var entityId = entity.Prototype?.ID;
                if (entityId != null
                    && layerProp.Id != null
                    && layerProp.Id.Contains(entityId))
                {
                    layer = layerProp.Layer;
                    return true;
                }

                if (layerProp.Tags != null && entity.HasAnyTag(layerProp.Tags))
                {
                    layer = layerProp.Layer;
                    return true;
                }
            }

            layer = null;
            return false;
        }

        public static bool HasItemMap(this SharedItemCounterComponent self)
        {
            return self._mapLayers.Count > 0;
        }

        public static bool HasTagCount(this SharedItemCounterComponent self)
        {
            return self._countTag != null;
        }


        public static int Count(this SharedItemCounterComponent comp)
        {
            var x = comp.Owner;
            x.TryGetComponent(out SharedStorageComponent? y);

            return 0;
        }
    }
}
