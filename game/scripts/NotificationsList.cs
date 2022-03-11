using Godot;
using System;
using System.Collections.Generic;
using therorogame.scripts;

public class NotificationsList : Control
{
    [Export] private PackedScene ItemScene;

    public override void _Ready()
    {
        Events events = (Events) GetNode(AutoloadPath.EVENTS_PATH);
        events.Connect(nameof(Events.EmitNotification), this, nameof(AddNotification));
    }

    public void AddNotification(Notification notification)
    {
        NotificationItem item = ItemScene.Instance<NotificationItem>();
        item.Icon = notification.texture;
        item.Description = notification.text;
        item.Timeout = notification.timeout;
        GetNode<VBoxContainer>("list").AddChild(item);
    }
}