using Godot;
using System;

public class Player : Area2D
{
    [Export] public int Speed = 400;
    private Vector2 ScreenSize;

    public override void _Ready()
    {
        ScreenSize = GetViewportRect().Size;
    }

    public override void _Process(float delta)
    {
        var velocity = Vector2.Zero;
        velocity.x = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
        velocity.y = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up");

        var animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        if (velocity.Length() > 0)
        {
            velocity = velocity.Normalized() * Speed;
            animatedSprite.Play();
        }
        else
        {
            animatedSprite.Stop();
        }

        Position += velocity * delta;
        Position = new Vector2(
            x: Mathf.Clamp(Position.x, 0, ScreenSize.x),
            y: Mathf.Clamp(Position.y, 0, ScreenSize.y)
        );
    }
}