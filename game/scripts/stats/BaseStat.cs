using Godot;

namespace therorogame.scripts.stats
{
    public class BaseStat<T> : Node, IBaseStat
    {
        [Signal]
        public delegate void UpdateValue(T NewValue);

        [Export] public T DefaultValue = default;

        protected T _statValue;

        public virtual T StatValue
        {
            get => StatValue;
            set
            {
                _statValue = FilterValue(value);
                EmitSignal(nameof(UpdateValue), _statValue);
            }
        }

        public virtual void ResetStat()
        {
            StatValue = DefaultValue;
        }

        public virtual T FilterValue(T value)
        {
            return value;
        }
    }
}