using Godot;
using System;
using therorogame.scripts;

public class ShopTrigger : Node
{
    public override void _Process(float delta)
    {
        InteractableTrigger trigger = GetNode<InteractableTrigger>("Interactable");
        if (Input.IsActionJustPressed("ui_interact") && trigger.IsTrigger)
        {
            // trigger.DisableCollision();
            trigger.IsTrigger = false;
            Events events = (Events) GetNode(AutoloadPath.EVENTS_PATH);
            events.EmitSignal(nameof(Events.TriggerShop));
        }
    }
}