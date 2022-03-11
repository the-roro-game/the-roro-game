using Godot;
using therorogame.data;

namespace therorogame.scripts.stats
{
    public class LifeStat : BaseStat<int>
    {
        [Signal]
        public delegate void UpdateLife(int NewLife, int MaxLife);

        public override void _Ready()
        {
            Events events = (Events) GetNode(AutoloadPath.EVENTS_PATH);
            events.Connect(nameof(Events.CharacterChange), this, nameof(OnCharacterChange));
            events.Connect(nameof(Events.TakeDamage), this, nameof(OnTakeDamage));
            EmitSignal(nameof(UpdateLife), _statValue, MaxLife);
        }

        public LifeStat()
        {
            DefaultValue = 0;
        }

        public override int StatValue
        {
            get => _statValue;
            set
            {
                _statValue = FilterValue(value);
                EmitSignal(nameof(UpdateLife), _statValue, MaxLife);
            }
        }

        private int _maxLife;

        [Export]
        public int MaxLife
        {
            get => _maxLife;
            set
            {
                _maxLife = value;
                EmitSignal(nameof(UpdateLife), _statValue, _maxLife);
            }
        }

        public override int FilterValue(int value)
        {
            return Mathf.Min(value, MaxLife);
        }

        public void OnCharacterChange(BaseCharacter newCharacter)
        {
            StatValue = newCharacter.Health;
            MaxLife = newCharacter.MaxHealth;
            GD.Print("mais");
        }

        public void OnTakeDamage(int damage)
        {
            GD.Print("lol");
            StatValue -= damage;
        }
    }
}