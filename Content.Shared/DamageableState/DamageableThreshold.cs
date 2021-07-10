using Content.Shared.DamageableState.Triggers;
using Robust.Shared.Serialization.Manager.Attributes;

namespace Content.Shared.DamageableState
{
    [DataDefinition]
    public class DamageableThreshold
    {
        [DataField("trigger", required: true)] public IDamageableTrigger DamageTrigger;

        [DataField("behavior", required: true)]
        public IBehavior Behavior = default!;

        [DataField("reversible")] public readonly bool IsReversible = true;

        public DamageableThreshold()
        {
            DamageTrigger = new SharedDamageableTrigger(0);
        }

        public DamageableThreshold(int damage, IDamageCategory? anyDamage = null)
        {
            DamageTrigger = anyDamage switch
            {
                DamageTypeCategory dmgCategory => new SharedDamageableTypeTrigger(dmgCategory.DamageType, damage),
                _ => new SharedDamageableTrigger(damage)
            };
        }
    }
}
