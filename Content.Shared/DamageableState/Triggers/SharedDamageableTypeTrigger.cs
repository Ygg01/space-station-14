using System;
using Content.Shared.Damage;
using Robust.Shared.Serialization.Manager.Attributes;

namespace Content.Shared.DamageableState.Triggers
{
    /// <summary>
    ///     A trigger that will activate when the amount of damage received
    ///     is above the specified threshold and of specified type.
    /// </summary>
    [Serializable]
    [DataDefinition]
    public class SharedDamageableTypeTrigger : IDamageableTrigger
    {
        /// <summary>
        ///     The amount of damage at which this threshold will trigger.
        /// </summary>
        [DataField("damage")]
        public int Damage { get; private set; }

        /// <summary>
        ///     The amount of damage at which this threshold will trigger.
        /// </summary>
        [DataField("damage")]
        public DamageType Type { get; private set; }

        /// <inheritdoc />
        int IDamageableTrigger.GetDamage()
        {
            return Damage;
        }
        /// <inheritdoc />
        IDamageCategory IDamageableTrigger.GetDamageCategory()
        {
            return new DamageTypeCategory(Type);
        }

        public SharedDamageableTypeTrigger(DamageType type, int damage)
        {
            Type = type;
            Damage = damage;
        }
    }
}
