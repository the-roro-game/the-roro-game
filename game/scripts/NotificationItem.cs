using Godot;
using System;

public class NotificationItem : HBoxContainer
{
    private Texture _icon;

    public Texture Icon
    {
        get => _icon;
        set
        {
            _icon = value;
            GetNode<TextureRect>("icon").Texture = value;
        }
    }

    private string _desc;

    public string Description
    {
        get => _desc;
        set
        {
            _desc = value;
            GetNode<Label>("description").Text = value;
        }
    }

    private int _timeout;

    public int Timeout
    {
        get => _timeout;
        set
        {
            _timeout = value;
            GetNode<Timer>("Timer").WaitTime = value;
        }
    }


    public override void _Ready()
    {
        Timer timer = GetNode<Timer>("Timer");
        timer.Connect("timeout", this, nameof(OnTimeout));
        timer.Start();
    }

    public void OnTimeout()
    {
        QueueFree();
    }
}