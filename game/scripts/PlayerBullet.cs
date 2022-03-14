using Godot;
using System;

public class PlayerBullet : Bullet
{
    [Export] public int SpeedIncr;
    private int BaseSpeed;

    public override void _Ready()
    {
        BaseSpeed = Speed;
    }

    public override void _Process(float delta)
    {
        Position += Transform.x * Speed * delta;
        Speed += SpeedIncr;
        if (Speed <= BaseSpeed + SpeedIncr * 10)
        {
            _sprite.Animation = "first";
        }
        else if (Speed > BaseSpeed + SpeedIncr * 10 && Speed <= BaseSpeed + SpeedIncr * 20)
        {
            _sprite.Animation = "second";
        }
        else
        {
            _sprite.Animation = "final";
        }
    }
}