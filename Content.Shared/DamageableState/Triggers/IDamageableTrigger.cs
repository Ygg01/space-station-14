namespace Content.Shared.DamageableState.Triggers
{
    public interface IDamageableTrigger
    {
        public int GetDamage();
        IDamageCategory GetDamageCategory();
    }
}
