using Content.Shared.Storage.EntitySystems;

namespace Content.Shared.Storage.Components
{
    [RegisterComponent]
    [Access(typeof(SharedItemMapperSystem))]
    public sealed class ItemMapperComponent : Component
    {
        [DataField("mapLayers")] public readonly Dictionary<string, SharedMapLayerData> MapLayers = new();
    }
}
