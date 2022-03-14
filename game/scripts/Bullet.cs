using Godot;
using System;

public class Bullet : KinematicBody2D
{
    const int Speed = 100;
    public Vector2 Velocity = new Vector2(0, 0);
    private Sprite _sprite;

    public override void _Ready()
    {
        GetNode<VisibilityNotifier2D>("notifier").Connect("screen_exited", this, nameof(OnScreenExited));
        _sprite = GetNode<Sprite>("Sprite");
    }

    public override void _Process(float delta)
    {
        _sprite.RotationDegrees = (_sprite.RotationDegrees + 3) % 360;
    }

    public override void _PhysicsProcess(float delta)
    {
        MoveAndCollide(Velocity.Normalized() * delta * Speed);
    }

    public void OnScreenExited()
    {
        QueueFree();
    }
}