using Godot;
using System;

public class Mob : RigidBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var animSprites = GetNode<AnimatedSprite>("AnimatedSprite");
        animSprites.Playing = true;
        string[] mobTypes = animSprites.Frames.GetAnimationNames();
        animSprites.Animation = mobTypes[GD.Randi() % mobTypes.Length];
    }

    public void OnScreenExited()
    {
        QueueFree();
    }
}