using Content.Shared.GameObjects.Components;
using Content.Shared.GameObjects.Components.Storage;
using Content.Shared.GameObjects.EntitySystems;
using JetBrains.Annotations;
using Robust.Server.GameObjects;
using Robust.Shared.Containers;

namespace Content.Server
{
    // [UsedImplicitly]
    // public class ServerStorageMapSystem : SharedStorageMapSystem
    // {
    //     protected override void UpdateSprite(ContainerModifiedMessage args, bool show)
    //     {
    //         if (args.Container.Owner.TryGetComponent(out AppearanceComponent? appearanceComponent)
    //             && args.Container.Owner.TryGetComponent(out SharedStorageMapComponent? storageMapComponent)
    //             && storageMapComponent.TryFindEntity(args.Entity, out var layer))
    //         {
    //             appearanceComponent.SetData(StorageMapVisual.LayerName, layer);
    //             appearanceComponent.SetData(StorageMapVisual.Show, show);
    //         }
    //     }
    // }
}
