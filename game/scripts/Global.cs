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
        }
    }

    public override void _Ready()
    {
        Events events = (Events) GetNode(AutoloadPath.EVENTS_PATH);
        events.Connect(nameof(Events.CharacterChange), this, nameof(UpdateCurrentCharacter));
    }


    public void UpdateCurrentCharacter(BaseCharacter newCharacter)
    {
        CurrCharacter = newCharacter;
    }
}