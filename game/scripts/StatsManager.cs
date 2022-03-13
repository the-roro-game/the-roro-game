using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using therorogame.data;

namespace therorogame.scripts
{
    public class StatHandler<T>
    {
        public delegate T ApplyModifiers(StatHandler<T> stat);

        public List<BaseStatModifier> modifiers = new List<BaseStatModifier>();
        public T baseValue;

        ApplyModifiers _applyModifiers;


        public T Value => _applyModifiers(this);

        public StatHandler(ApplyModifiers applyModifiers)
        {
            _applyModifiers = applyModifiers;
        }

        public static int IntStatModifier(StatHandler<int> statHandler)
        {
            int final = statHandler.baseValue;
            foreach (BaseStatModifier statModifier in statHandler.modifiers)
            {
                final = final + (statModifier.ModifierAdder * (statModifier.ModifierType == "+" ? 1 : -1));
            }

            return final;
        }
    }

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
            CreateStat<int>("LifeStat", 0, StatHandler<int>.IntStatModifier);
            CreateStat<int>("MaxLifeStat", 100, StatHandler<int>.IntStatModifier);
            CreateStat<int>("CoinsStat", 0, StatHandler<int>.IntStatModifier);

            StatChanged();
        }


        public void CreateStat<T>(string statName, T defaultValue, StatHandler<T>.ApplyModifiers applyModifiers)
        {
            StatHandler<T> handler = new StatHandler<T>(applyModifiers);
            handler.baseValue = defaultValue;
            _stats.Add(statName, handler);
        }

        public StatHandler<T> GetStat<T>(string statName)
        {
            if (_stats.ContainsKey(statName))
            {
                return (StatHandler<T>) _stats[statName];
            }

            return default;
        }

        public void UpdateStat<T>(string statName, T newValue)
        {
            if (_stats.ContainsKey(statName))
            {
                ((StatHandler<T>) _stats[statName]).baseValue = newValue;
                GD.Print("new coins:",((StatHandler<T>) _stats[statName]).Value);
                StatChanged();
            }
        }

        public void AddModifier<T>(BaseStatModifier baseStatModifier)
        {
            GetStat<T>(baseStatModifier.StatName).modifiers.Add(baseStatModifier);
            StatChanged();
        }

        public int ModifiersCount<T>(string statName)
        {
            return GetStat<T>(statName).modifiers.Count;
        }

        private void OnCharacterChange(BaseCharacter newCharacter)
        {
            UpdateStat("LifeStat", newCharacter.Health);
            UpdateStat("MaxLifeStat", newCharacter.MaxHealth);
        }

        private void OnTakeDamage(int damage)
        {
            int LifeStat = GetStat<int>("LifeStat").Value;
            UpdateStat("LifeStat", LifeStat - damage);
        }

        private void OnGetMoney(int money)
        {
            int CoinsStat = GetStat<int>("CoinsStat").Value;
            UpdateStat("CoinsStat", CoinsStat + money);
        }

        public void StatChanged()
        {
            EmitSignal(nameof(StatsChanged));
        }
    }
}