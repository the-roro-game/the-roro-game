using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using therorogame.data;

namespace therorogame.scripts
{
    public class StatsManager : Node
    {
        [Signal]
        public delegate void StatsChanged();

        private readonly Dictionary<string, object> _stats = new Dictionary<string, object>();


        public override void _EnterTree()
        {
            Events events = (Events) GetNode(AutoloadPath.EVENTS_PATH);
            events.Connect(nameof(Events.CharacterChange), this, nameof(OnCharacterChange));
            events.Connect(nameof(Events.TakeDamage), this, nameof(OnTakeDamage));
            events.Connect(nameof(Events.GiveMoney), this, nameof(OnGetMoney));
            CreateStat<int>("LifeStat", 0);
            CreateStat<int>("MaxLifeStat", 100);
            CreateStat<int>("CoinsStat", 0);

            StatChanged();
        }


        public void CreateStat<T>(string statName, T defaultValue)
        {
            _stats.Add(statName, defaultValue);
        }

        public T GetStat<T>(string statName)
        {
            if (_stats.ContainsKey(statName))
            {
                return (T) _stats[statName];
            }

            return default;
        }

        public void UpdateStat<T>(string statName, T newValue)
        {
            if (_stats.ContainsKey(statName))
            {
                _stats[statName] = newValue;
                StatChanged();
            }
        }

        private void OnCharacterChange(BaseCharacter newCharacter)
        {
            UpdateStat("LifeStat", newCharacter.Health);
            UpdateStat("MaxLifeStat", newCharacter.MaxHealth);
        }

        private void OnTakeDamage(int damage)
        {
            int LifeStat = GetStat<int>("LifeStat");
            UpdateStat("LifeStat", LifeStat - damage);
        }

        private void OnGetMoney(int money)
        {
            int CoinsStat = GetStat<int>("CoinsStat");
            UpdateStat("CoinsStat", CoinsStat + money);
        }

        public void StatChanged()
        {
            EmitSignal(nameof(StatsChanged));
        }
    }
}