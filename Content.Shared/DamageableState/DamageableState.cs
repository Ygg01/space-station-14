using System.Collections.Generic;
using Robust.Shared.GameObjects;
using Robust.Shared.Serialization.Manager.Attributes;

namespace Content.Shared.DamageableState
{
    public class SharedDamageableStateComponent : Component
    {
        public override string Name => "DamageState";

        [DataField("thresholds")] public List<DamageableThreshold> _Thresholds = new();

        public DamageTracker DamageTracker = new();
    }

    [DataDefinition]
    public class DamageableThreshold
    {
        [DataField("trigger", required: true)] public IDamageableTrigger DamageTrigger = new SharedDamageableTrigger(0);

        [DataField("behavior", required: true)]
        public IBehavior? Behavior;

        [DataField("reversible")] public readonly bool IsReversible = true;
    }
}
