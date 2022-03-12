using Godot;
using System;

public class Bullet : Node2D
{
    const int Speed = 100;

    public override void _Ready()
    {
        GetNode<VisibilityNotifier2D>("notifier").Connect("screen_exited", this, nameof(OnScreenExited));
    }

    public override void _Process(float delta)
    {
        Position += Transform.x * Speed * delta;
    }

    public void OnScreenExited()
    {
        QueueFree();
    }
}