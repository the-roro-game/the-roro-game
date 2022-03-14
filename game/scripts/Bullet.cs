using Godot;
using System;

public class Bullet : Node2D
{
    [Export] public int Speed = 100;
    public Vector2 Velocity = new Vector2(0, 0);
    protected AnimatedSprite _sprite;

    public override void _EnterTree()
    {
        GetNode<VisibilityNotifier2D>("notifier").Connect("screen_exited", this, nameof(OnScreenExited));
        _sprite = GetNode<AnimatedSprite>("AnimatedSprite");
    }

    public override void _Process(float delta)
    {
        Position += Transform.x * Speed * delta;
        _sprite.RotationDegrees = (_sprite.RotationDegrees + 3) % 360;
    }


    public void OnScreenExited()
    {
        QueueFree();
    }
}