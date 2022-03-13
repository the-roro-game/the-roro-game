namespace therorogame.scripts
{
    public interface Damageable
    {
        void MakeDamages(int damages);

        bool CanTakeDamage();
    }
}