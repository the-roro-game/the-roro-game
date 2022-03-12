using Godot;
using therorogame.data;

namespace therorogame.scripts
{
    public class StatsManager : Node
    {
        [Signal]
        public delegate void StatsChanged();

        private int _maxLife;

        [Export]
        public int MaxLife
        {
            get => _maxLife;
            set
            {
                _maxLife = value;
                EmitSignal(nameof(StatsChanged));
            }
        }

        private int _life;

        [Export]
        public int Life
        {
            get => _life;
            set
            {
                _life = value;
                EmitSignal(nameof(StatsChanged));
            }
        }

        private int _coins;

        [Export]
        public int Coins
        {
            get => _coins;
            set
            {
                _coins = value;
                EmitSignal(nameof(StatsChanged));
            }
        }

        public override void _Ready()
        {
            Events events = (Events) GetNode(AutoloadPath.EVENTS_PATH);
            events.Connect(nameof(Events.CharacterChange), this, nameof(OnCharacterChange));
            events.Connect(nameof(Events.TakeDamage), this, nameof(OnTakeDamage));
            events.Connect(nameof(Events.GiveMoney), this, nameof(OnGetMoney));
            EmitSignal(nameof(StatsChanged));
        }

        public void OnCharacterChange(BaseCharacter newCharacter)
        {
            MaxLife = newCharacter.MaxHealth;
            Life = newCharacter.Health;
        }

        public void OnTakeDamage(int damage)
        {
            Life -= damage;
        }

        public void OnGetMoney(int money)
        {
            Coins += money;
        }
    }
}