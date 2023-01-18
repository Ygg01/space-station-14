using Content.Server.Fluids.EntitySystems;
using Content.Shared.Chemistry.Components;
using Content.Shared.Chemistry.Reaction;
using Content.Shared.Chemistry.Reagent;
using Content.Shared.FixedPoint;
using JetBrains.Annotations;
using Robust.Shared.Map;

namespace Content.Server.Chemistry.TileReactions;

[UsedImplicitly]
[DataDefinition]
public sealed class SpillIfPuddlePresentTileReaction : ITileReaction
{

    public FixedPoint2 TileReact(TileRef tile, ReagentPrototype reagent, FixedPoint2 reactVolume)
    {
        var spillableSystem = IoCManager.Resolve<IEntityManager>().System<SpillableSystem>();

        if (reactVolume < 5 || !spillableSystem.TryGetPuddle(tile, out _))
        {
            return FixedPoint2.Zero;
        }

        return spillableSystem.SpillAt(tile, new Solution(reagent.ID, reactVolume), "PuddleSmear", noTileReact: true) != null
            ? reactVolume
            : FixedPoint2.Zero;
    }
}
