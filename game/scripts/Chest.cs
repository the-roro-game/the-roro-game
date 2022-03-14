using Godot;
using System;
using therorogame.scripts;

public class Chest : Node2D
{
    [Export] public BaseItem item = null;
    [Export] public int Quantity;
    private bool opened = false;


    public override void _Ready()
    {
        ItemsManager im = (ItemsManager) GetNode(AutoloadPath.ITEMS_MANAGER);
        if (!opened)
        {
            item = im.GetRandomItem();
        }

        if (opened)
        {
            GetNode<AnimatedSprite>("sprite").Animation = "open";

        }
    }

    public override void _Process(float delta)
    {
        InteractableTrigger trigger = GetNode<InteractableTrigger>("Interactable");

        if (Input.IsActionJustPressed("ui_interact") && trigger.IsTrigger && !opened)
        {
            opened = true;

            Events events = (Events) GetNode<Events>(AutoloadPath.EVENTS_PATH);
            GetNode<AnimatedSprite>("sprite").Animation = "open";
            trigger.DisableCollision();
            Notification notification = new Notification(item.Icon, $"give {item.Name}", 2);
            if (item.SignalName != "")
            {
                events.EmitSignal(item.SignalName, 1);
            }

            events.EmitSignal(nameof(Events.EmitNotification), notification);
        }
    }
}