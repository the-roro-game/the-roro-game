using Godot;
using System;
using therorogame.scripts;

public class Chest : Node2D
{
    [Export] public BaseItem item = null;
    private bool opened = false;


    public override void _Ready()
    {
        ItemsManager im = (ItemsManager) GetNode("/root/im");
        item = im.GetRandomItem();
    }

    public override void _Process(float delta)
    {
        InteractableTrigger trigger = GetNode<InteractableTrigger>("Interactable");

        if (Input.IsActionJustPressed("ui_interact") && trigger.IsTrigger && !opened)
        {
           
            opened = true;

            Events events = (Events) GetNode<Events>("/root/events");
            GetNode<AnimatedSprite>("sprite").Animation = "open";
            trigger.DisableCollision();
            Notification notification = new Notification(item.Icon, $"give {item.Name}", 2);

            events.EmitSignal(nameof(Events.EmitNotification), notification);
        }
    }
}