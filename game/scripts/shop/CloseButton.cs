using Godot;
using System;
using therorogame.scripts;

public class CloseButton : Button
{
    [Export] public NodePath nodeToClose;

    public override void _Ready()
    {
        Connect("pressed", this, nameof(OnClose));
    }

    public void OnClose()
    {
        Events events = (Events) GetNode(AutoloadPath.EVENTS_PATH);
        events.EmitSignal(nameof(Events.CloseHud));
        GetNode(nodeToClose).QueueFree();
    }
}