using Godot;
using System;
using therorogame.scripts;

public class InteractableTrigger : Node2D
{
    [Export] public string InteractName;

    public bool IsTrigger = false;

    public override void _Ready()
    {
        Connect("body_entered", this, nameof(TriggerEnter));
        Connect("body_exited", this, nameof(TriggerExit));
    }

    public override void _Process(float delta)
    {
    }

    public void TriggerEnter(Node body)
    {
        Events events = (Events) GetNode("/root/events");
        events.EmitSignal(nameof(Events.TriggerInteract), InteractName);
        IsTrigger = true;
    }

    public void TriggerExit(Node body)
    {
        Events events = (Events) GetNode("/root/events");
        events.EmitSignal(nameof(Events.ExitInteract));
        IsTrigger = true;
    }

    public void DisableCollision()
    {
        GetNode<CollisionShape2D>("trigger").Disabled = true;
    }
}