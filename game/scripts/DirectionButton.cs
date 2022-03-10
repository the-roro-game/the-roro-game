using Godot;
using System;
using therorogame.scripts;

public class DirectionButton : Node2D
{
    public int XOffset = 0;
    public int YOffset = 0;


    public override void _Process(float delta)
    {
        InteractableTrigger trigger = GetNode<InteractableTrigger>("Interactable");
        if (Input.IsActionJustPressed("ui_interact") && trigger.IsTrigger)
        {
            Events events = (Events) GetNode("/root/events");
            events.EmitSignal(nameof(Events.DirectionChange), XOffset, YOffset);
        }
    }
}