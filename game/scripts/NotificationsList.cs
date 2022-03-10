using Godot;
using System;
using System.Collections.Generic;
using therorogame.scripts;

public class NotificationsList : Control
{
    [Export] private PackedScene ItemScene;


    public void AddNotification(Notification notification)
    {
        NotificationItem item = ItemScene.Instance<NotificationItem>();
        item.Icon = notification.texture;
        item.Description = notification.text;
        item.Timeout = notification.timeout;
        AddChild(item);
    }
}