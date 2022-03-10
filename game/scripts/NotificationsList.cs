using Godot;
using System;
using System.Collections.Generic;

public class NotificationsList : Control
{
    public Queue<NotificationItem> NotificationQueue = new Queue<NotificationItem>();
    [Export] private PackedScene ItemScene;

    public override void _Ready()
    {
    }

    public void AddNotification(Texture texture, string text, int timeout)
    {
        NotificationItem item = ItemScene.Instance<NotificationItem>();
        // item.
    }
}