using Content.Shared.GameObjects.Components.Storage;
using Robust.Shared.GameObjects;

namespace Content.Client.GameObjects.Components.Storage
{
    [RegisterComponent]
    [ComponentReference(typeof(SharedItemCounterComponent))]
    public class ItemCounterComponent : SharedItemCounterComponent
    {

    }
}
