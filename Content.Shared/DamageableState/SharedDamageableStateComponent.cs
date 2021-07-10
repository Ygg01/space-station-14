using System;
using System.Collections.Generic;
using Content.Shared.Damage;
using Content.Shared.DamageableState.Triggers;
using Robust.Shared.GameObjects;
using Robust.Shared.Serialization;
using Robust.Shared.Serialization.Manager.Attributes;

namespace Content.Shared.DamageableState
{
    public class SharedDamageableStateComponent : Component, ISerializationHooks
    {
        public override string Name => "DamageState";
        [DataField("thresholds")] private List<DamageableThreshold> _thresholds = new();

        public List<DamageableThreshold> Thresholds
        {
            set
            {
                _thresholds = value;
                ((ISerializationHooks) this).AfterDeserialization();
            }
        }

        public Dictionary<IDamageCategory, ThresholdLevels> ActiveThresholds = new();
        public Dictionary<DamageType, DamageLevels> DamageTracker = new();

        void ISerializationHooks.AfterDeserialization()
        {
            ActiveThresholds.Clear();
            foreach (var threshold in _thresholds)
            {
                var damageCategory = threshold.DamageTrigger.GetDamageCategory();
                if (!ActiveThresholds.TryGetValue(damageCategory, out var damageLevels))
                {
                    damageLevels = new ThresholdLevels();
                    ActiveThresholds[damageCategory] = damageLevels;
                }

                damageLevels.AddThreshold(threshold);
                damageLevels.ActiveThreshold = null;
            }
        }
    }


    /// <summary>
    /// Represents what damage levels are there and which threshold is currently active
    ///
    /// It has two invariants:
    ///  - DamageThresholds are ordered from lowest to highest trigger damage
    ///  - There is one Threshold per trigger damage
    /// </summary>
    public class ThresholdLevels
    {
        private readonly List<DamageableThreshold> _orderedThresholds = new();
        public IReadOnlyList<DamageableThreshold> OrderedThresholds => _orderedThresholds;
        public DamageableThreshold? ActiveThreshold;

        public void AddThreshold(DamageableThreshold threshold)
        {
            var insertPos = 0; // default insert position
            for (int i = _orderedThresholds.Count - 1; i >= 0; i--)
            {
                if (_orderedThresholds[i].DamageTrigger.GetDamage() == threshold.DamageTrigger.GetDamage())
                {
                    throw new ArgumentException(
                        $"Damage trigger with that damage ({threshold.DamageTrigger.GetDamage()}) already exists");
                }

                if (_orderedThresholds[i].DamageTrigger.GetDamage() < threshold.DamageTrigger.GetDamage())
                {
                    insertPos = i + 1; // insert after biggest element
                    break;
                }
            }

            _orderedThresholds.Insert(insertPos, threshold);
        }
    }

    public struct DamageLevels
    {
        public int CurrentDamage;
    }
}
