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
        Tween tween = new Tween();
        tween.Name = "Tween";
        tween.InterpolateProperty(this, "modulate:a", Modulate.a, 0, 1);
        tween.InterpolateProperty(this, "rect_position:x", RectPosition.x, RectPosition.x - 100, 1,
            Tween.TransitionType.Back);
        tween.Connect("tween_completed", this, nameof(OnTweenFinished));
        AddChild(tween);
        tween.Start();
    }

    public void OnTweenFinished(Godot.Object obj, NodePath key)
    {
        Tween tween = GetNode<Tween>("Tween");
        RemoveChild(tween);
        tween.QueueFree();
        QueueFree();
    }
}