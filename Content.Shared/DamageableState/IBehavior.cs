namespace Content.Shared.DamageableState
{
    public interface IBehavior
    {
        void Execute();
        void Revert() { }
    }
}
