using System;
using System.Collections.Generic;
using Content.Shared.Damage;

namespace Content.Shared.DamageableState
{
    public class DamageTracker
    {
        private readonly List<int> _trackDamage = new(Enum.GetNames<DamageType>().Length);

        private int IndexOf(DamageType damageType) =>
            damageType switch
            {
                DamageType.Blunt => 0,
                DamageType.Slash => 1,
                DamageType.Piercing => 2,
                DamageType.Heat => 3,
                DamageType.Shock => 4,
                DamageType.Cold => 5,
                DamageType.Poison => 6,
                DamageType.Radiation => 7,
                DamageType.Asphyxiation => 8,
                DamageType.Bloodloss => 9,
                DamageType.Cellular => 10,
                _ => throw new ArgumentException($"Unhandled damage type {damageType}. Add to switch statement above"),
            };

        public void AddDamage(DamageType damageType, ushort amount)
        {
            _trackDamage[IndexOf(damageType)] += amount;
        }

        public void RemoveDamage(DamageType damageType, ushort amount)
        {
            _trackDamage[IndexOf(damageType)] -= amount;
        }

        public void SetDamage(DamageType damageType, int amount)
        {
            _trackDamage[IndexOf(damageType)] = amount;
        }

        public int GetDamage(DamageType damageType)
        {
            return _trackDamage[IndexOf(damageType)];
        }

        public int GetDamages(ISet<DamageType> damageTypes)
        {
            var sum = 0;
            foreach (var dmgType in damageTypes)
            {
                sum += _trackDamage[IndexOf(dmgType)];
            }

            if (sum < 0)
            {
                sum = 0;
            }

            return sum;
        }
    }
}
