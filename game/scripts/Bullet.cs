using Godot;
using System;

public class Bullet : Node2D
{
    [Export]public  int Speed = 100;
    public Vector2 Velocity = new Vector2(0, 0);
    private Sprite _sprite;

    public override void _Ready()
    {
        GetNode<VisibilityNotifier2D>("notifier").Connect("screen_exited", this, nameof(OnScreenExited));
        _sprite = GetNode<Sprite>("Sprite");
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