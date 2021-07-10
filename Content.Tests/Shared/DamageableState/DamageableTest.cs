using System;
using System.Collections;
using System.Collections.Generic;
using Content.Shared.Damage;
using Content.Shared.DamageableState;
using NUnit.Framework;

namespace Content.Tests.Shared.DamageableState
{
    [TestFixture]
    [Parallelizable]
    public class DamageableStateTest
    {
        private static IEnumerable<List<DamageableThreshold>> EnsureSortingLists()
        {
            yield return new List<DamageableThreshold>
            {
                new(100, IDamageCategory.AnyDamage)
            };
            yield return new List<DamageableThreshold>
            {
                new(200, IDamageCategory.AnyDamage),
                new(100, IDamageCategory.AnyDamage)
            };
            yield return new List<DamageableThreshold>
            {
                new(210),
                new(110),
                new(220, new DamageTypeCategory(DamageType.Bloodloss)),
                new(120, new DamageTypeCategory(DamageType.Bloodloss)),
                new(320, new DamageTypeCategory(DamageType.Bloodloss)),
                new(230, new DamageTypeCategory(DamageType.Cold)),
                new(130, new DamageTypeCategory(DamageType.Cold)),
                new(430, new DamageTypeCategory(DamageType.Cold)),
                new(330, new DamageTypeCategory(DamageType.Cold))
            };
        }

        private IComparer SortByDamage = Comparer<DamageableThreshold>.Create(
            (x, y) =>
            {
                if (ReferenceEquals(x, y)) return 0;
                if (ReferenceEquals(null, y)) return 1;
                if (ReferenceEquals(null, x)) return -1;
                return x.DamageTrigger.GetDamage().CompareTo(y.DamageTrigger.GetDamage());
            }
        );

        [Test]
        [TestCaseSource(nameof(EnsureSortingLists))]
        public void EnsureOrdering(List<DamageableThreshold> damages)
        {
            var damageableComponent = new SharedDamageableStateComponent();

            damageableComponent.Thresholds = new List<DamageableThreshold>(damages);
            foreach (var levels in damageableComponent.ActiveThresholds.Values)
            {
                CollectionAssert.IsOrdered(levels.OrderedThresholds, SortByDamage);
            }
        }

        [Test]
        public void EnsureOneDamagePerLevel()
        {
            var damageableComponent = new SharedDamageableStateComponent();

            Assert.Catch(typeof(ArgumentException), () =>
            {
                damageableComponent.Thresholds = new List<DamageableThreshold>(new[]
                {
                    new DamageableThreshold(100),
                    new DamageableThreshold(200),
                    new DamageableThreshold(100),
                });
            });
        }
    }
}
