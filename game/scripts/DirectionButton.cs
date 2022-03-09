using Godot;
using System;
using therorogame.scripts;

public class DirectionButton : Button
{
    public int XOffset = 0;
    public int YOffset = 0;

    public override void _Ready()
    {
        Connect("pressed", this, nameof(OnDirectionPressed));
    }

    public void OnDirectionPressed()
    {
        Events events = (Events) GetNode("/root/events");
        events.EmitSignal(nameof(Events.DirectionChange), XOffset, YOffset);
    }
}