using System;
using Robust.Shared.Serialization.Manager.Attributes;

namespace Content.Shared.DamageableState
{
    /// <summary>
    ///     A trigger that will activate when the amount of damage received
    ///     is above the specified threshold.
    /// </summary>
    [Serializable]
    [DataDefinition]
    public class SharedDamageableTrigger : IDamageableTrigger
    {
        /// <summary>
        ///     The amount of damage at which this threshold will trigger.
        /// </summary>
        [DataField("damage")]
        public int Damage { get; set; }

        public SharedDamageableTrigger(int damage)
        {
            Damage = damage;
        }
    }
}
