using System.Collections.Generic;
using Content.Shared.Damage;
using Robust.Shared.GameObjects;

namespace Content.Shared.DamageableState
{
    public class DamageableDeltaEvent : EntityEventArgs
    {
        public IReadOnlyDictionary<DamageType, DamageDelta> DamageChanges;

        public DamageableDeltaEvent(Dictionary<DamageType, DamageDelta> damageChanges)
        {
            DamageChanges = damageChanges;
        }
    }

    public struct DamageDelta
    {
        /// <summary>
        ///     Type of damage that changed.
        /// </summary>
        public DamageType Type;

        /// <summary>
        ///     How much the health value changed from its last value (negative is heals, positive is damage).
        /// </summary>
        public int Delta;

        public bool IsNegative => Delta < 0;
    }
}
