using System.Collections.Generic;
using Content.Shared.Damage;

namespace Content.Shared.DamageableState
{
    public interface IDamageCategory
    {
        public static IDamageCategory AnyDamage = new AnyDamageCategory();
        int CalculateDamage(Dictionary<DamageType, DamageLevels> damageTracker);
    }

    public sealed record DamageTypeCategory(DamageType DamageType) : IDamageCategory
    {
        public int CalculateDamage(Dictionary<DamageType, DamageLevels> damageTracker)
        {
            return damageTracker[DamageType].CurrentDamage;
        }
    }

    internal sealed record AnyDamageCategory : IDamageCategory
    {
        public int CalculateDamage(Dictionary<DamageType, DamageLevels> damageTracker)
        {
            var sum = 0;
            foreach (var dmgLevel in damageTracker.Values)
            {
                sum += dmgLevel.CurrentDamage;
            }

            return sum;
        }
    }
}
