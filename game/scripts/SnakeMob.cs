using Godot;
using System;

public class SnakeMob : KinematicBody2D
{
    private KinematicBody2D player = null;

    private int speed = 100;

    private Vector2 motion = Vector2.Zero;

    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public override void _PhysicsProcess(float delta)
    {
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
