using JetBrains.Annotations;
using Robust.Shared.GameObjects;

namespace Content.Shared.DamageableState.System
{
    [UsedImplicitly]
    public class SharedDamageableStateSystem : EntitySystem
    {
        public override void Initialize()
        {
            base.Initialize();

            SubscribeLocalEvent<SharedDamageableStateComponent, DamageableDeltaEvent>(OnDamage);
        }

        public void OnDamage(EntityUid uid, SharedDamageableStateComponent component, DamageableDeltaEvent args)
        {
            if (args.DamageChanges.Count <= 0)
                return;

            foreach (var (dmgType,change) in args.DamageChanges)
            {
                if (change.Delta == 0)
                    continue;

                var damage = component.DamageTracker[dmgType];
                damage.CurrentDamage += change.Delta;
            }

            component.TriggerBehavior();
        }
    }

    public static class DamageableHelper
    {
        private static DamageableThreshold? RecalculateLevel(
            this SharedDamageableStateComponent sharedDamageableStateComponent, IDamageCategory category)
        {
            var damage = category.CalculateDamage(sharedDamageableStateComponent.DamageTracker);
            var thresholds = sharedDamageableStateComponent.ActiveThresholds[category];

            for (var i = thresholds.OrderedThresholds.Count - 1; i >= 0; i--)
            {
                if (damage >= thresholds.OrderedThresholds[i].DamageTrigger.GetDamage())
                {
                    return thresholds.OrderedThresholds[i];
                }
            }

            return null;
        }

        public static void TriggerBehavior(this SharedDamageableStateComponent component)
        {
            foreach (var (category, levels) in component.ActiveThresholds)
            {
                var newActive = component.RecalculateLevel(category);
                if (newActive != levels.ActiveThreshold && levels.ActiveThreshold is { IsReversible: true })
                {
                    levels.ActiveThreshold.Behavior.OnExit();
                }

                newActive?.Behavior.OnEnter();
            }
        }
    }
}
