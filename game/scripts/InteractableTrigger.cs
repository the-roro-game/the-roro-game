using Godot;
using System;
using therorogame.scripts;

public class InteractableTrigger : Node2D
{
    [Export] public string InteractName = "";

    [Export] public string GroupTrigger = "";

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
        if (GroupTrigger == "" || body.IsInGroup(GroupTrigger))
        {
            GD.Print("interactable");
            Events events = (Events) GetNode(AutoloadPath.EVENTS_PATH);
            events.EmitSignal(nameof(Events.TriggerInteract), InteractName);
            IsTrigger = true;
        }
    }

    public void TriggerExit(Node body)
    {
        Events events = (Events) GetNode(AutoloadPath.EVENTS_PATH);
        events.EmitSignal(nameof(Events.ExitInteract));
        IsTrigger = false;
    }

    public void DisableCollision()
    {
        GetNode<CollisionShape2D>("trigger").Disabled = true;
    }
}