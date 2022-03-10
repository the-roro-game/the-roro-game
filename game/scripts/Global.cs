using Godot;
using System;
using therorogame.data;
using therorogame.scripts;

public class Global : Node
{
    private BaseCharacter _currCharacter;

    public BaseCharacter CurrCharacter
    {
        get => _currCharacter;
        set
        {
            GD.Print("changing character:", value.Name);
            _currCharacter = value;
            UpdateLife(_currCharacter.Health, _currCharacter.MaxHealth);
        }
    }

    private int _currCoins;

    public int CurrCoins
    {
        get => _currCoins;
        set
        {
            GD.Print("new coins",value);
            _currCoins = value;
            
        }
    }


    public override void _Ready()
    {
        Events events = (Events) GetNode("/root/events");
        events.Connect(nameof(Events.CharacterChange), this, nameof(UpdateCurrentCharacter));
    }

    public void UpdateLife(int NewLife, int MaxLife)
    {
        Events events = (Events) GetNode("/root/events");
        CurrCharacter.Health = Math.Min(NewLife, CurrCharacter.MaxHealth);
        events.EmitSignal(nameof(Events.PlayerLifeChange), CurrCharacter.Health, CurrCharacter.MaxHealth);
    }
    public void UpdateCoins(int NewCoins)
    {
        // Events events = (Events) GetNode("/root/events");
        // CurrCharacter.Health = Math.Min(NewLife, CurrCharacter.MaxHealth);
        // events.EmitSignal(nameof(Events.PlayerLifeChange), CurrCharacter.Health, CurrCharacter.MaxHealth);
    }


    public void UpdateCurrentCharacter(BaseCharacter newCharacter)
    {
        CurrCharacter = newCharacter;
    }
}